using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(myTherapist.Startup))]
namespace myTherapist
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
