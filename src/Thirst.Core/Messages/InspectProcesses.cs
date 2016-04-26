using System.Collections.Generic;

namespace Thirst.Core.Messages
{
    public class InspectProcesses
    {
        public IEnumerable<string> ProcessNames { get; private set; }

        public InspectProcesses(IEnumerable<string> processNames)
        {
            ProcessNames = processNames;
        }
    }
}
