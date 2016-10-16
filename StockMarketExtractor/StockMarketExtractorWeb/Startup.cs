using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StockMarketExtractorWeb.Startup))]
namespace StockMarketExtractorWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
