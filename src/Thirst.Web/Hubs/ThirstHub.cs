using Akka.Actor;
using Microsoft.AspNet.SignalR;
using Thirst.Core.Hubs;
using Thirst.Core.Messages;
using Thirst.Web.Actors;

namespace Thirst.Web.Hubs
{
    public class ThirstHub : Hub, IThirstHub
    {
        public void SendRunningProcesses(RunningProcesses message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ThirstHub>();

            context.Clients.All.getRunningProcesses(message);
        }

        public void InspectProcesses(InspectProcesses message)
        {
            SystemActors.MasterActor.Tell(message, ActorRefs.Nobody);
        }
    }
}
