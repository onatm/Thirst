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
    public class When_handling_running_services_message : TestKit
    {
        private readonly Mock<IThirstHub> mockThirstHub = new Mock<IThirstHub>();

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var masterActor = Sys.ActorOf(Props.Create(() => new MasterActor(ActorRefs.Nobody, mockThirstHub.Object)));

            masterActor.Tell(new RunningServices(string.Empty, new List<string>()));
            
            ExpectNoMsg(100);
        }

        [Test]
        public void it_should_call_SendRunningServices_method()
        {
            mockThirstHub.Verify(x => x.SendRunningServices(It.IsAny<RunningServices>()));
        }
    }
}
