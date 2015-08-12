using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Kpmg.Datas.Contexts;
using Kpmg.Datas.Factories;

namespace Kpmg
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Repositories.Load(new Repositories());
            Repositories.LoadContext(new KpmgContext());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}