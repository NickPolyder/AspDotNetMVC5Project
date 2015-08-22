using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(SynMetal_MVC.Startup))]
namespace SynMetal_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }
    }
}
