using System.Web.Mvc;
using System.Web.Routing;
using FizzWare.NBuilder;
using Microsoft.Practices.ServiceLocation;
using SolrNet;

namespace SolrTesting
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            InitializeSolr();
        }

        private void InitializeSolr()
        {
            Startup.Init<SolrTesting.Models.Order>("http://localhost:8983/solr");
            
            var operation = ServiceLocator.Current.GetInstance<ISolrOperations<SolrTesting.Models.Order>>();

            operation.Delete(SolrQuery.All);

            var orders = Builder<SolrTesting.Models.Order>.CreateListOfSize(25).Build();

            operation.Add(orders);

            operation.Commit();
        }
    }
}