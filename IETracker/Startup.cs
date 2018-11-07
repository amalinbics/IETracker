using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IETracker.Startup))]
namespace IETracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
