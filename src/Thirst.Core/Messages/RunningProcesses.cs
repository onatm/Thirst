using System.Collections.Generic;

namespace Thirst.Core.Messages
{
    public class RunningProcesses
    {
        public string HostName { get; private set; }

        public IEnumerable<string> ProcessNames { get; private set; }

        public RunningProcesses(string hostName, IEnumerable<string> processNames)
        {
            HostName = hostName;
            ProcessNames = processNames;
        }
    }
}
