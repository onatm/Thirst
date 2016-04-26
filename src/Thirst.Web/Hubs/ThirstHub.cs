using Akka.Actor;
using Microsoft.AspNet.SignalR;
using Thirst.Core.Hubs;
using Thirst.Core.Messages;
using Thirst.Web.Actors;

namespace Thirst.Web.Hubs
{
    public class ThirstHub : Hub, IThirstHub
    {
        public void SendRunningServices(RunningServices message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ThirstHub>();

            context.Clients.All.getRunningServices(message);
        }

        public void InspectServices(InspectServices message)
        {
            SystemActors.MasterActor.Tell(message, ActorRefs.Nobody);
        }
    }
}
