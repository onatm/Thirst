using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.Event;
using Thirst.Shared.Messages;

namespace Thirst.Master.Actors
{
    public class MasterActor : ReceiveActor
    {
        private readonly ILoggingAdapter logger = Context.GetLogger();

        private readonly IActorRef masterBroadcaster;
        private ICancelable broadcastTask;

        public MasterActor(IActorRef masterBroadcaster)
        {
            this.masterBroadcaster = masterBroadcaster;
            Context.Watch(this.masterBroadcaster);

            Receive<RunningServices>(m =>
            {
                logger.Info("Hostname: {0}, Services: {1}", m.HostName, m.ServiceNames.Aggregate((x, y) => x + ", " + y));
            });
        }

        protected override void PreStart()
        {
            broadcastTask =
                Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(
                    TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), masterBroadcaster, new InspectServices(new List<string> { "Thirst.Agent.vshost" }), Context.Self);

            base.PreStart();
        }

        protected override void PostStop()
        {
            broadcastTask.Cancel();

            Context.Stop(masterBroadcaster);

            base.PostStop();
        }
    }
}
