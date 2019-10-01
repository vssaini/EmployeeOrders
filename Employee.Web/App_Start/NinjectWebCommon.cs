[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Employee.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Employee.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Employee.Web.App_Start
{
    using System;
    using System.Web;
    using Employee.Infrastructure;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
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
            // Ninject uses kernel to resolve dependenices
            var kernel = new StandardKernel();
            try
            {
                // A binding is an instruction which maps one type (usually an abstract type or
                // an interface) to a concrete type that matches such a given type.This process is also
                // called Service Registration.  
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Bind interface to concrete type UnitOfWork
            // Request scope is useful in web applications when we need to get a single instance
            // of a type from Ninject as long as we are handling the same request
            kernel.Bind<IUnitOfWork>().ToMethod(ctx => new UnitOfWork(new DatabaseContext())).InRequestScope();
        }
    }
}
