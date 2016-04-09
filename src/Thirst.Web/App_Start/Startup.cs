using Owin;

namespace Thirst.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseNancy(options => options.Bootstrapper = new Bootstrapper());
        }  
    }
}
