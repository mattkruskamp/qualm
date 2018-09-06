using Qualm.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Qualm.Tests.Commands
{
    public class CommandHandlerFactoryTests
    {
        private CommandHandlerFactory BuildCommandHandlerFactory()
        {
            return new CommandHandlerFactory();
        }

        [Fact]
        public void Create_NullHandler_ThrowsException()
        {
            var factory = BuildCommandHandlerFactory();

            Assert.Throws<InvalidOperationException>(() => factory.Create(null));
        }

        [Fact]
        public void Create_NonCommandHandlerType_ThrowsException()
        {
            var factory = BuildCommandHandlerFactory();

            Assert.Throws<InvalidOperationException>(() =>
                factory.Create(typeof(string)));
        }

        [Fact]
        public void Create_CommandHandler_NotNull()
        {
            var factory = BuildCommandHandlerFactory();

            var handler = factory.Create(typeof(TestCommandHandler));

            Assert.NotNull(handler);
        }
    }
}
