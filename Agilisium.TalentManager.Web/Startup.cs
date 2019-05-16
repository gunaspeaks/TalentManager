using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Agilisium.TalentManager.Web.Startup))]
namespace Agilisium.TalentManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
