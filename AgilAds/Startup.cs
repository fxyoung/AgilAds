using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AgilAds.Startup))]
namespace AgilAds
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
