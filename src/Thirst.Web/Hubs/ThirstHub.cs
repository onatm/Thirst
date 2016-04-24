using Microsoft.AspNet.SignalR;
using Thirst.Core.Hubs;
using Thirst.Core.Messages;

namespace Thirst.Web.Hubs
{
    public class ThirstHub : Hub, IThirstHub
    {
        public void SendRunningServices(RunningServices message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ThirstHub>();

            context.Clients.All.getRunningServices(message);
        }
    }
}
