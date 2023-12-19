using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MBM.UI.Startup))]
namespace MBM.UI
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            //ConfigureAuth(app);
        }
    }
}
