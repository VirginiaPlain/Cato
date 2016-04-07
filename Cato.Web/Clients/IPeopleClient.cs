

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cato.Web.Models;

namespace Cato.Web.Clients
{
    public interface IPeopleClient
    {

        Task<IEnumerable<PersonViewModel>> GetPeopleAsync();

        Task<PersonViewModel> GetPersonAsync(string personId);
        Task<PersonImagesViewModel> GetPersonImages(string personId);
        Task<PersonViewModel> AddPersonAsync(PersonViewModel pvm);

        Task<string> AddPersonImageAsync(string personId, Stream imageStream, string contentType);

        Task<PersonDatabaseStatusViewModel> GetPersonDatabaseStatus();
    }
}
