using System.Collections.Generic;
using Akka.Actor;
using Akka.TestKit.NUnit;
using Moq;
using NUnit.Framework;
using Thirst.Core.Actors;
using Thirst.Core.Hubs;
using Thirst.Core.Messages;

namespace Thirst.Core.UnitTests.Master
{
    [TestFixture]
    public class When_handling_inspect_services_message : TestKit
    {
        private IActorRef masterActor;
        private Mock<IActorRef> mockMasterBroadcast;

        private InspectServices expectedInspectServicesMessage;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            mockMasterBroadcast = new Mock<IActorRef>(MockBehavior.Strict);
            mockMasterBroadcast.As<IInternalActorRef>();
            mockMasterBroadcast.Setup(x => x.Equals(It.IsAny<IActorRef>())).Returns(true);

            expectedInspectServicesMessage = new InspectServices(new List<string> { "ExpectedProcessName" });

            masterActor = Sys.ActorOf(Props.Create(() => new MasterActor(mockMasterBroadcast.Object, new Mock<IThirstHub>().Object)));

            masterActor.Tell(expectedInspectServicesMessage);

            ExpectNoMsg();
        }

        [Test]
        public void it_should_forward_the_message()
        {
            mockMasterBroadcast.Verify(x => x.Tell(expectedInspectServicesMessage, masterActor));
        }
    }
}
