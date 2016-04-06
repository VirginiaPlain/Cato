

using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cato.Shared.CatoServices;
using Cato.Web.App_Start;

namespace Cato.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            

        }
    }
}
