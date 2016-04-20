using Akka.Actor;
using Akka.Routing;
using Owin;
using Thirst.Core.Actors;
using Thirst.Core.Services;
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

            var broadcaster =
                ActorSystemRefs.ActorSystem.ActorOf(Props.Create(() => new AgentActor(new ProcessService())).WithRouter(FromConfig.Instance), "commander");

            SystemActors.MasterActor = ActorSystemRefs.ActorSystem.ActorOf(Props.Create(() => new MasterActor(broadcaster)), "master");
        }
    }
}
