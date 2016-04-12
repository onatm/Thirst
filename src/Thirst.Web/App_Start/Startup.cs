using Akka.Actor;
using Owin;
using Thirst.Web.Actors;

namespace Thirst.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            app.UseNancy(options => options.Bootstrapper = new Bootstrapper());

            ActorSystemRefs.ActorSystem = ActorSystem.Create("Thirst");
        }
    }
}
