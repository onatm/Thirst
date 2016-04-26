using Akka.Actor;
using Thirst.Core.Hubs;
using Thirst.Core.Messages;

namespace Thirst.Core.Actors
{
    public class MasterActor : ReceiveActor
    {
        private readonly IActorRef masterBroadcaster;
        private readonly IThirstHub thirstHub;

        public MasterActor(IActorRef masterBroadcaster, IThirstHub thirstHub)
        {
            this.masterBroadcaster = masterBroadcaster;
            this.thirstHub = thirstHub;

            Context.Watch(masterBroadcaster);

            Receive<InspectProcesses>(m =>
            {
                masterBroadcaster.Tell(m);
            });

            Receive<RunningProcesses>(m =>
            {
                this.thirstHub.SendRunningProcesses(m);
            });
        }

        protected override void PostStop()
        {
            Context.Stop(masterBroadcaster);

            base.PostStop();
        }
    }
}
