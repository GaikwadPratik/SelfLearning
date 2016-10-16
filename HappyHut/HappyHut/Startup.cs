using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HappyHut.Startup))]
namespace HappyHut
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
