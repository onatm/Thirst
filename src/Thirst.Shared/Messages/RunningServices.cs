using System.Collections.Generic;

namespace Thirst.Shared.Messages
{
    public class RunningServices
    {
        public string HostName { get; private set; }

        public IEnumerable<string> ServiceNames { get; private set; }

        public RunningServices(string hostName, IEnumerable<string> serviceNames)
        {
            HostName = hostName;
            ServiceNames = serviceNames;
        }
    }
}
