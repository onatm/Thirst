using Akka.Actor;
using Akka.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Owin;
using Thirst.Core.Actors;
using Thirst.Core.Hubs;
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

            var hubManager = new DefaultHubManager(GlobalHost.DependencyResolver);
            var thirstHub = hubManager.ResolveHub("ThirstHub") as IThirstHub;

            ActorSystemRefs.ActorSystem = ActorSystem.Create("Thirst");

            var broadcaster =
                ActorSystemRefs.ActorSystem.ActorOf(Props.Create(() =>
                    new AgentActor(new ProcessService())).WithRouter(FromConfig.Instance), "commander");

            SystemActors.MasterActor = ActorSystemRefs.ActorSystem.ActorOf(Props.Create(() => new MasterActor(broadcaster, thirstHub)), "master");
        }
    }
}
