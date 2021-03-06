[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Ignite.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Ignite.App_Start.NinjectWebCommon), "Stop")]

namespace Ignite.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Microsoft.AspNet.Identity.Owin;
    using Ignite.Data;
    using Ignite.Admin.Services.Interfaces;
    using Ignite.Admin.Services;
    using Ignite.Services.Contracts;
    using Ignite.Services;
    using Ignite.Areas.Admin.Services;
    using Ignite.Areas.Admin.Services.Interfaces;

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
            var kernel = new StandardKernel();
            try
            {
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
            kernel.Bind<ApplicationUserManager>()
                .ToMethod(_ => HttpContext.Current
                .GetOwinContext()
                .GetUserManager<ApplicationUserManager>());

            kernel.Bind<ApplicationDbContext>()
                .ToMethod(_ => HttpContext.Current
                .GetOwinContext()
                .GetUserManager<ApplicationDbContext>());

            kernel.Bind<IUploadCourseService>().To<UploadCourseService>();
            kernel.Bind<IUserCourseService>().To<UserCoursesService>();
            kernel.Bind<IStatisticsService>().To<StatisticsService>();
            kernel.Bind<IQuizService>().To<QuizService>();
            kernel.Bind<IAssignmentService>().To<AssignmentService>();
        }        
    }
}
