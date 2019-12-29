using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VehicleAssistance.Startup))]
namespace VehicleAssistance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
