using Cato.Shared.CatoServices;
using System;
using System.Collections.Generic;
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



    }


}