using Akka.Actor;
using Thirst.Core.Messages;

namespace Thirst.Core.Actors
{
    public class MasterActor : ReceiveActor
    {
        private readonly IActorRef masterBroadcaster;

        public MasterActor(IActorRef masterBroadcaster)
        {
            this.masterBroadcaster = masterBroadcaster;

            Context.Watch(masterBroadcaster);

            Receive<InspectServices>(m =>
            {
                masterBroadcaster.Tell(m);
            });

            Receive<RunningServices>(m =>
            {

            });
        }

        protected override void PostStop()
        {
            Context.Stop(masterBroadcaster);

            base.PostStop();
        }
    }
}
