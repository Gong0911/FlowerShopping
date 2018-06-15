using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlowerShopping.Startup))]
namespace FlowerShopping
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
