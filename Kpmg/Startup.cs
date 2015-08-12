using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kpmg.Startup))]
namespace Kpmg
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
