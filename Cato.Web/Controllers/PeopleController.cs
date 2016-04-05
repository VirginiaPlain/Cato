

using System.Threading.Tasks;
using System.Web.Mvc;
using Cato.Shared.CatoServices;
using Cato.Shared.Services.PersonStore;
using Cato.Web.Clients;
using Cato.Web.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;

namespace Cato.Web.Controllers
{
    public class PeopleController : Controller
    {

        private readonly IPeopleClient _peopleClient;

        public PeopleController()
        {
            // move this to unity & constructor
            string connStr = CloudConfigurationManager.GetSetting("AzureTable.ConnectionString");
            CloudStorageAccount csa = CloudStorageAccount.Parse(connStr);
            IPeopleService svc = new CatoPeopleService("key", new AzurePersonStore(csa));
            _peopleClient = new PeopleClient(svc);
        }

        // GET: People
        public async Task<ActionResult> Index()
        {

            var data = await _peopleClient.GetPeopleAsync();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(PersonViewModel pvm)
        {
            await Task.Delay(10);
            return RedirectToAction("Index");
        }
    }
}