using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Calends.Startup))]
namespace Calends
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
