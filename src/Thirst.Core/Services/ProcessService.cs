using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Thirst.Core.Services
{
    public interface IProcessService
    {
        IEnumerable<string> GetProcessNames();
    }

    public class ProcessService : IProcessService
    {
        public IEnumerable<string> GetProcessNames()
        {
            return Process.GetProcesses().Select(p => p.ProcessName);
        }
    }
}
