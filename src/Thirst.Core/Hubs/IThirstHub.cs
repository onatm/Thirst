using Thirst.Core.Messages;

namespace Thirst.Core.Hubs
{
    public interface IThirstHub
    {
        void SendRunningServices(RunningServices message);
    }
}
