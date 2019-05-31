using Agilisium.TalentManager.Model;
//using Agilisium.TalentManager.PostgresModel;
using Agilisium.TalentManager.Web.App_Start;
using Agilisium.TalentManager.Web.Helpers;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Agilisium.TalentManager.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            //// Un-Comment below line to insert the default data
            //System.Data.Entity.Database.SetInitializer(new TalentManagerSeedData());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrapper.Run();

            Application[UIConstants.CONFIG_ENABLE_PAGINATION] = ConfigurationManager.AppSettings[UIConstants.CONFIG_ENABLE_PAGINATION];
            Application[UIConstants.CONFIG_RECORDS_PER_PAGE] = ConfigurationManager.AppSettings[UIConstants.CONFIG_RECORDS_PER_PAGE];
            Application[UIConstants.CONFIG_EMAIL_TEMPLATES_FOLDER_PATH] = ConfigurationManager.AppSettings[UIConstants.CONFIG_EMAIL_TEMPLATES_FOLDER_PATH];
            Application[UIConstants.CONFIG_ADMIN_USER_NAME] = ConfigurationManager.AppSettings[UIConstants.CONFIG_ADMIN_USER_NAME];
            Application[UIConstants.CONFIG_IGNORABLE_TEXT_IN_USER_NAME] = ConfigurationManager.AppSettings[UIConstants.CONFIG_IGNORABLE_TEXT_IN_USER_NAME];
        }
    }
}
