using System.Collections.Generic;

namespace Thirst.Shared.Messages
{
    public class InspectServices
    {
        public IEnumerable<string> ServiceNames { get; private set; }

        public InspectServices(IEnumerable<string> serviceNames)
        {
            ServiceNames = serviceNames;
        }
    }
}
