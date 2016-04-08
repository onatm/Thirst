using Akka.Actor;
using Topshelf;

namespace Thirst.Agent
{
    public class AgentService : ServiceControl
    {
        protected ActorSystem ClusterSystem { get; set; }

        public bool Start(HostControl hostControl)
        {
            ClusterSystem = ActorSystem.Create("Thirst");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ClusterSystem.Terminate();
            return true;
        }
    }
}
