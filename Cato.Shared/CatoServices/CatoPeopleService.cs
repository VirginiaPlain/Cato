

using Cato.Shared.FaceApi;
using Cato.Shared.Models;
using Cato.Shared.Services.PersonStore;
using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face.Contract;
using Newtonsoft.Json;
using Person = Cato.Shared.Models.Person;

namespace Cato.Shared.CatoServices
{
    public class CatoPeopleService : IPeopleService
    {
        private readonly FaceServiceClient _faceApi;
        private readonly IPersonStore _personStore;
        private const string PERSON_GROUP_ID = "alexone";

        private static bool _apiIsInitialsed = false;

        public CatoPeopleService(string key, IPersonStore pStore)
        {
            _faceApi = new FaceServiceClient(key);
            _personStore = pStore;
            if (!_apiIsInitialsed)
            {
                Task<bool> i = Task.Run(() => Initialise());
                _apiIsInitialsed = i.Result;
            }
        }


        public async Task<Person> AddPersonAsync(Person person)
        {
            var result = await _faceApi.CreatePersonAsync(PERSON_GROUP_ID, person.Name);
            person.FaceApiPersonId = result.PersonId.ToString();

            _personStore.SavePerson(person);

            return person;
        }

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            var t = await Task.Run<IEnumerable<Person>>(() => _personStore.GetPeople());
            return t;
        }

        public async Task<Person> GetPersonAsync(string personId)
        {
            var t = await Task.Run<Person>(() => _personStore.GetPerson(personId));
            return t;
        }

        public async Task<IEnumerable<PersonImage>> GetPersonImagesAsync(string personId)
        {
            var t = await Task.Run<IEnumerable<PersonImage>>(() => _personStore.GetPersonImages(personId));

            // todo Get Urls

            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="imageStream"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public async Task<PersonImage> AddPersonFaceAsync(string personId, Stream imageStream, string contentType)
        {
            
            var person = _personStore.GetPerson(personId);
            var image = new PersonImage()
            {
                PersonId = personId,
                ImageId  = Guid.NewGuid().ToString()
            };
            _personStore.SavePersonImage(image.ImageId, imageStream, contentType);

            imageStream.Position = 0;
            var fdr = await DetectFacesAsync(imageStream);

            if (fdr.FaceCount == 0)
            {
                throw new Exception("No faces detected in image");
            }

            // If more than one face use the first (MS Api returns largest face first)
            var activeFace = fdr.Faces.First();
            
            imageStream.Position = 0;
            var fr = new FaceRectangle()
            {
                Height = activeFace.Height,
                Left = activeFace.Left,
                Top = activeFace.Top,
                Width = activeFace.Width
            };

            string imgUrl = _personStore.GetImageUrl(image.ImageId);
            var r = await _faceApi.AddPersonFaceAsync(PERSON_GROUP_ID, Guid.Parse(person.FaceApiPersonId), imgUrl, null, fr);
            image.FaceDetection = JsonConvert.SerializeObject(activeFace);
            image.FaceApiFaceId = r.PersistedFaceId.ToString();
            _personStore.SavePersonImageData(image);

            await Train();
            
            return image;
        }


        public async Task<FaceDetectionResult> DetectFacesAsync(Stream imageStream)
        {
            var rtn = new FaceDetectionResult();

            var r = await _faceApi.DetectAsync(imageStream);
            foreach(Microsoft.ProjectOxford.Face.Contract.Face f in r)
            {
                var d = new FaceDetection()
                {
                    FaceId = f.FaceId.ToString(),
                    Left = f.FaceRectangle.Left,
                    Top = f.FaceRectangle.Top,
                    Width = f.FaceRectangle.Width,
                    Height = f.FaceRectangle.Height
                };
                rtn.Faces.Add(d);
            }
            return rtn;
        }

        public async Task<bool> Train()
        {
            await _faceApi.TrainPersonGroupAsync(PERSON_GROUP_ID);
            return true;
        }

        public async Task<FaceApiTrainingStatus> GetTrainingStatusAsync()
        {
            var status = await _faceApi.GetPersonGroupTrainingStatusAsync(PERSON_GROUP_ID);
            var rtn = new FaceApiTrainingStatus()
            {
                Status = status.Status.ToString(),
                LastAction = status.LastActionDateTime.ToString("D"),
                Message = status.Message
            };
            return rtn;
        }

        public async Task<bool> Initialise()
        {
            bool personGroupExists = false;
            var x = await _faceApi.GetPersonGroupsAsync();
            foreach (PersonGroup pg in x)
            {
                if (pg.PersonGroupId.Equals(PERSON_GROUP_ID))
                {
                    personGroupExists = true;
                }
            }
            if (!personGroupExists)
            {
                await _faceApi.CreatePersonGroupAsync(PERSON_GROUP_ID, "Cato people");
            }
            return true;
        }

    }


    public interface IPeopleService
    {
        Task<bool> Initialise();
        Task<Person> AddPersonAsync(Person p);
        Task<PersonImage> AddPersonFaceAsync(string personId, Stream imageStream, string contentType);
        //void DeletePersonFace(string personId, string faceId);

        // todo
        Task<FaceDetectionResult> DetectFacesAsync(Stream imageStream);
        //FaceIdentificationResult IdentifyFaces(FaceDetectionResult faces, double minThreshold);
        Task<IEnumerable<Person>> GetPeopleAsync();
        Task<Person> GetPersonAsync(string personId);

        Task<IEnumerable<PersonImage>> GetPersonImagesAsync(string personId);

        Task<bool> Train();
        Task<FaceApiTrainingStatus> GetTrainingStatusAsync();
        // obs
        //void CreatePersonGroup(string groupId, string name);
        //bool PersonGroupExists(string groupId);
        //Task<bool> PersonGroupExistsAsync(string groupId);

        //Task StartTrainPersonGroupAsync();
        //Task<string> GetPersonGroupTrainStatusAsync();
    }

}
