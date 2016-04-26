using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Thirst.Core.Messages;
using Thirst.Core.Services;

namespace Thirst.Core.Actors
{
    public class AgentActor : ReceiveActor
    {
        public AgentActor(IProcessService processService)
        {
            Receive<InspectProcesses>(m =>
            {
                var processes = processService.GetProcessNames();

                IEnumerable<string> requestedProcesses;

                if (m.ProcessNames == null)
                {
                    requestedProcesses = new List<string>();
                }
                else
                {
                    requestedProcesses = processes.Intersect(m.ProcessNames);
                }

                var runningProcesses = new RunningProcesses(Environment.MachineName, requestedProcesses.ToList());

                Sender.Tell(runningProcesses);
            });
        }
    }
}
