using Moq;
using Qualm.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Qualm.Tests.Commands
{
    public class CommandProcessorTests
    {
        private CommandProcessor BuildCommandProcessor()
        {
            var registry = new Mock<ICommandHandlerRegistry>();
            registry.Setup(m => m.GetHandler(It.IsAny<Type>()))
                .Returns(typeof(TestCommandHandler));

            var factory = new Mock<ICommandHandlerFactory>();
            factory.Setup(m => m.Create(It.IsAny<Type>()))
                .Returns(new TestCommandHandler());

            return new CommandProcessor(registry.Object, factory.Object);
        }

        [Fact]
        public async Task ExecuteAsync_NullCommand_ThrowsException()
        {
            var processor = BuildCommandProcessor();

            await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await processor.ExecuteAsync(null));
        }

        [Fact]
        public async Task ExecuteAsync_Command_ChangesCommand()
        {
            var processor = BuildCommandProcessor();
            var command = new TestCommand();

            await processor.ExecuteAsync(command);

            Assert.NotEqual(1, command.CommandNumber);
        }
    }
}
