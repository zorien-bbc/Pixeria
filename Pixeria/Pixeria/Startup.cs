using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pixeria.Startup))]
namespace Pixeria
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
