using Cato.Shared.CatoServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Cato.Shared.Models;
using Cato.Web.Models;

namespace Cato.Web.Clients
{
    public class PeopleClient : IPeopleClient
    {

        private readonly IPeopleService _peopleSvc;


        public PeopleClient(IPeopleService peepSvc)
        {
            _peopleSvc = peepSvc;
        }


        public async Task<IEnumerable<PersonViewModel>> GetPeopleAsync()
        {
            var list = new List<PersonViewModel>();

            var x = await _peopleSvc.GetPeopleAsync();
            foreach (Person p in x)
            {
                var vm = new PersonViewModel()
                {
                    PersonId = p.PersonId,
                    Name = p.Name,
                    FriendlyName = p.FriendlyName,
                    FaceApiPersonId = p.FriendlyName
                };
                list.Add(vm);
            }

            return list;
        }

        public async Task<PersonViewModel> GetPersonAsync(string personId)
        {
            var x = await _peopleSvc.GetPersonAsync(personId);
            var vm = new PersonViewModel()
            {
                PersonId = x.PersonId,
                Name = x.Name,
                FriendlyName = x.FriendlyName
            };

            return vm;
        }

        public async Task<PersonImagesViewModel> GetPersonImages(string personId)
        {
            var model = new PersonImagesViewModel();

            var x = await _peopleSvc.GetPersonImagesAsync(personId);
            foreach (PersonImage pi in x)
            {
                var vm = new PersonImageViewModel()
                {
                    PersonId = pi.PersonId,
                    ImageId = pi.ImageId,
                    FaceDetection = pi.FaceDetection,
                    FaceApiFaceId = pi.FaceApiFaceId,
                    ImageUrl = pi.ImageUrl
                };
                model.Images.Add(vm);
            }

            return model;
        }

        public async Task<PersonViewModel> AddPersonAsync(PersonViewModel pvm)
        {
            pvm.PersonId = Guid.NewGuid().ToString();
            var person = new Person()
            {
                PersonId = pvm.PersonId,
                Name = pvm.Name,
                FriendlyName = pvm.FriendlyName
            };
            var p = await _peopleSvc.AddPersonAsync(person);

            var pvmResult = new PersonViewModel()
            {
                PersonId = p.PersonId,
                Name = p.Name,
                FriendlyName = p.FriendlyName,
                FaceApiPersonId = p.FaceApiPersonId
            };
            return pvmResult;
        }

        public async Task<string> AddPersonImageAsync(string personId, Stream imageStream, string contentType)
        {
            var x = await _peopleSvc.AddPersonFaceAsync(personId, imageStream, contentType);

            return x.ImageId;
        }

    }


}