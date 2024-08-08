using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCProject_Nazmul.Startup))]
namespace MVCProject_Nazmul
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
