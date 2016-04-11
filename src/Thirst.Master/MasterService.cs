using Akka.Actor;
using Akka.Routing;
using Thirst.Core.Actors;
using Thirst.Master.Actors;
using Topshelf;

namespace Thirst.Master
{
    public class MasterService : ServiceControl
    {
        protected ActorSystem ClusterSystem { get; set; }

        public bool Start(HostControl hostControl)
        {
            ClusterSystem = ActorSystem.Create("Thirst");

            var broadcaster = ClusterSystem.ActorOf(Props.Create(() => new AgentActor()).WithRouter(FromConfig.Instance), "commander");

            ClusterSystem.ActorOf(Props.Create(() => new MasterActor(broadcaster)), "master");

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ClusterSystem.Terminate();
            return true;
        }
    }
}
