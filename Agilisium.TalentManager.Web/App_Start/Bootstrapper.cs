using Agilisium.TalentManager.ReportingService;
using Agilisium.TalentManager.Repository.Repositories;
using Agilisium.TalentManager.Service.Concreate;
using Agilisium.TalentManager.Web.Models;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            // Repositories
            builder.RegisterAssemblyTypes(typeof(DropDownCategoryRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Services
            builder.RegisterAssemblyTypes(typeof(DropDownCategoryService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Windows Services
            builder.RegisterAssemblyTypes(typeof(ContractorRequestProcessor).Assembly)
                .Where(t => t.Name.EndsWith("Processor"))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();

            // Server Utilities
            //builder.RegisterAssemblyTypes(typeof(EmailHandler).Assembly)
            //    .Where(t => t.Name.EndsWith("Handler"))
            //    .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}