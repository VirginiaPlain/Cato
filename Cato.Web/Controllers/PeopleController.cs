

using System.Threading.Tasks;
using System.Web.Mvc;
using Cato.Shared.CatoServices;
using Cato.Shared.Services.PersonStore;
using Cato.Web.Clients;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

namespace Cato.Web.Controllers
{
    public class PeopleController : Controller
    {

        private IPeopleClient _peopleClient;

        public PeopleController()
        {
            StorageCredentials sc = new StorageCredentials();
            CloudStorageAccount csa = new CloudStorageAccount(sc, true);
            IPeopleService svc = new CatoPeopleService("key", new AzurePersonStore(csa));
            _peopleClient = new PeopleClient(svc);
        }

        // GET: People
        public async Task<ActionResult> Index()
        {

            var data = await _peopleClient.GetPeopleAsync();

            return View();
        }
    }
}