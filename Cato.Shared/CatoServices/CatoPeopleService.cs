using Cato.Shared.FaceApi;
using Cato.Shared.Models;
using Cato.Shared.Services.PersonStore;
using Cato.Shared.Services.TableStorage.TableEntities;
using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cato.Shared.CatoServices
{
    public class CatoPeopleService : IPeopleService
    {
        private FaceServiceClient _faceApi;
        private readonly IPersonStore _personStore;
        private const string PERSON_GROUP_ID = "alexone";

        public CatoPeopleService(string key, IPersonStore pStore)
        {
            _faceApi = new FaceServiceClient(key);
            _personStore = pStore;
        }


        public async Task<Person> AddPersonAsync(Person person)
        {
            var result = await _faceApi.CreatePersonAsync(PERSON_GROUP_ID, person.Name);
            person.FaceApiPersonId = result.PersonId.ToString();

            _personStore.SavePerson(person);

            return person;
        }


        public async Task<string> AddPersonFaceAsync(string personId, Stream imageStream)
        {

            var r = await _faceApi.AddPersonFaceAsync(PERSON_GROUP_ID, "a",, null, null);

            return string.Empty;
        }

        public async Task<FaceDetectionResult> DetectFacesAsync(string imageUrl)
        {
            var rtn = new FaceDetectionResult();
            var r = await _faceApi.DetectAsync(imageUrl);
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

    }


    public interface IPeopleService
    {
        Task<Person> AddPersonAsync(Person p);
        Task<string> AddPersonFaceAsync(string personId, Stream imageStream);
        //void DeletePersonFace(string personId, string faceId);

        // todo
        Task<FaceDetectionResult> DetectFacesAsync(Stream imageStream);
        //FaceIdentificationResult IdentifyFaces(FaceDetectionResult faces, double minThreshold);



        // obs
        //void CreatePersonGroup(string groupId, string name);
        //bool PersonGroupExists(string groupId);
        //Task<bool> PersonGroupExistsAsync(string groupId);

        //Task StartTrainPersonGroupAsync();
        //Task<string> GetPersonGroupTrainStatusAsync();
    }

}
