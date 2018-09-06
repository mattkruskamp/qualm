using Microsoft.Extensions.DependencyInjection;
using Qualm.Commands;
using Qualm.Commands.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Qualm.Tests.Commands.DependencyInjection
{
    public class IServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddCommands_NoParams_RegistersDependencies()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddCommands();
            var provider = services.BuildServiceProvider();
            var registry = provider.GetService<ICommandHandlerRegistry>();
            var factory = provider.GetService<ICommandHandlerFactory>();
            var processor = provider.GetService<ICommandProcessor>();
        }
    }
}
