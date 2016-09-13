using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SampleSignalRRealTime.Startup))]
namespace SampleSignalRRealTime
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
