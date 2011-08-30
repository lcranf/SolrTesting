using Microsoft.Practices.ServiceLocation;
using Ninject.Integration.SolrNet;
using NinjectAdapter;
using SolrNet;
using SolrNet.Mapping;

[assembly: WebActivator.PreApplicationStartMethod(typeof(SolrTesting.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(SolrTesting.App_Start.NinjectMVC3), "Stop")]

namespace SolrTesting.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Mvc;

    public static class NinjectMVC3 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var mapper = new AllPropertiesMappingManager();
            mapper.SetUniqueKey(typeof(SolrTesting.Models.Order).GetProperty("Id"));

            kernel.Load(new [] { new SolrNetModule("http://localhost:8983/solr") { Mapper = mapper } });
            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));
        }
    }
}
