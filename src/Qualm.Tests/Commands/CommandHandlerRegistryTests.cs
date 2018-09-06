using Qualm.Commands;
using System;
using Xunit;

namespace Qualm.Tests.Commands
{
    public class CommandHandlerRegistryTests
    {
        private CommandHandlerRegistry BuildCommandHandlerRegistry()
        {
            return new CommandHandlerRegistry();
        }

        [Fact]
        public void Register_NullCommandOrType_ThrowsException()
        {
            var registry = BuildCommandHandlerRegistry();

            Assert.Throws<InvalidOperationException>(() =>
                registry.RegisterHandler(null, typeof(TestCommandHandler)));
            Assert.Throws<InvalidOperationException>(() =>
                registry.RegisterHandler(typeof(TestCommand), null));
        }

        [Fact]
        public void Register_NotCommandOrHandlerType_ThrowsException()
        {
            var registry = BuildCommandHandlerRegistry();

            Assert.Throws<InvalidOperationException>(() =>
                registry.RegisterHandler(typeof(string), typeof(TestCommandHandler)));
            Assert.Throws<InvalidOperationException>(() =>
                registry.RegisterHandler(typeof(TestCommand), typeof(string)));
        }

        [Fact]
        public void RegisterAndGetCommand_ValidCommand_ReturnsTestCommandHandler()
        {
            var registry = BuildCommandHandlerRegistry();

            registry.RegisterHandler(typeof(TestCommand), typeof(TestCommandHandler));
            var type = registry.GetHandler(typeof(TestCommand));

            Assert.Equal(typeof(TestCommandHandler), type);
        }

        [Fact]
        public void Get_NoCommand_ReturnsNull()
        {
            var registry = BuildCommandHandlerRegistry();

            var type = registry.GetHandler(typeof(string));

            Assert.Null(type);
        }

        [Fact]
        public void RegisterHandlers_ThisAssembly_GetsCommandHandler()
        {
            var registry = BuildCommandHandlerRegistry();

            registry.RegisterHandlers(typeof(TestCommand).Assembly);

            var type = registry.GetHandler(typeof(TestCommand));
            Assert.Equal(typeof(TestCommandHandler), type);
        }
    }
}
