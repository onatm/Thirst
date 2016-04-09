using Microsoft.AspNet.SignalR;

namespace Thirst.Web.Hubs
{
    public class ThirstHub : Hub
    {
        public void Welcome()
        {
            Clients.All.print("i'm thirsty!");
        }
    }
}
