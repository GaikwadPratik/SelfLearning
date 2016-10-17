using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AngularStart.Startup))]
namespace AngularStart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
