using Thirst.Core.Messages;

namespace Thirst.Core.Hubs
{
    public interface IThirstHub
    {
        void SendRunningProcesses(RunningProcesses message);

        void InspectProcesses(InspectProcesses message);
    }
}
