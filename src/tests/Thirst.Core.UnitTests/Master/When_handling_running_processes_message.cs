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
    public class When_handling_running_processes_message : TestKit
    {
        private readonly Mock<IThirstHub> mockThirstHub = new Mock<IThirstHub>();

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var masterActor = Sys.ActorOf(Props.Create(() => new MasterActor(ActorRefs.Nobody, mockThirstHub.Object)));

            masterActor.Tell(new RunningProcesses(string.Empty, new List<string>()));
            
            ExpectNoMsg(100);
        }

        [Test]
        public void it_should_call_SendRunningProcesses_method()
        {
            mockThirstHub.Verify(x => x.SendRunningProcesses(It.IsAny<RunningProcesses>()));
        }
    }
}
