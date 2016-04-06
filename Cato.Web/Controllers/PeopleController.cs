

using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Cato.Web.Clients;
using Cato.Web.Models;


namespace Cato.Web.Controllers
{
    public class PeopleController : Controller
    {

        private readonly IPeopleClient _peopleClient;

        public PeopleController(IPeopleClient client)
        {
            _peopleClient = client;// new PeopleClient(svc);
        }

        // GET: People
        public async Task<ActionResult> Index()
        {

            var data = await _peopleClient.GetPeopleAsync();

            return View(data);
        }

        public async Task<ActionResult> Person(string personId)
        {

            var x = await _peopleClient.GetPersonAsync(personId);

            var m = new PersonImagesViewModel()
            {
                PersonId = x.PersonId,
                Name = x.Name,
                FriendlyName = x.FriendlyName
            };

            return View(m);
        }

        [HttpPost]
        public async Task<ActionResult> Add(PersonViewModel pvm)
        {
            await _peopleClient.AddPersonAsync(pvm);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> AddPersonImage(HttpPostedFileBase file, string personId)
        {

            await _peopleClient.AddPersonImageAsync(personId, file.InputStream, file.ContentType);

            return RedirectToAction("Person", new {personId});
        }
    }
}