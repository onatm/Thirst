using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.TestKit.NUnit;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Thirst.Core.Actors;
using Thirst.Core.Messages;
using Thirst.Core.Services;

namespace Thirst.Core.UnitTests.Agent.When_handling_inspect_processes_message.with_more_than_one_expected_processes
{
    [TestFixture]
    public class and_some_of_them_are_present : TestKit
    {
        private const string ExpectedProcessName1 = "ExpectedProcessName1";
        private const string ExpectedProcessName2 = "ExpectedProcessName2";
        private RunningProcesses runningProcesses;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var mockProcessService = new Mock<IProcessService>();

            mockProcessService.Setup(x => x.GetProcessNames()).Returns(new List<string> { "RandomProcessName1", ExpectedProcessName2 });

            var agentActor = Sys.ActorOf(Props.Create(() => new AgentActor(mockProcessService.Object)));

            agentActor.Tell(new InspectProcesses(new List<string> { ExpectedProcessName1, ExpectedProcessName2 }));

            runningProcesses = ExpectMsg<RunningProcesses>();
        }

        [Test]
        public void it_should_send_running_processes_message()
        {
            runningProcesses.Should().NotBeNull();
        }

        [Test]
        public void the_message_should_have_host_name()
        {
            runningProcesses.HostName.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void the_message_should_have_correct_running_processes_count()
        {
            runningProcesses.ProcessNames.Count().Should().Be(1);
        }

        [Test]
        public void the_message_should_have_expected_process_name()
        {
            runningProcesses.ProcessNames.Contains(ExpectedProcessName2).Should().BeTrue();
        }
    }
}
