using Owin;

namespace Thirst.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            app.UseNancy(options => options.Bootstrapper = new Bootstrapper());
        }  
    }
}
