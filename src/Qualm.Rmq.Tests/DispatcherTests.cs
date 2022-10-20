using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Qualm.Rmq;
using Moq;
using Qualm.Queuing;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Qualm.Rmq.Tests
{
  public class DispatcherTests
  {
    readonly ILogger _logger;
    readonly ITestOutputHelper _output;

    public DispatcherTests(ITestOutputHelper output)
    {
      _output = output;
      _logger = _output.BuildLogger();
    }
    [Fact]
    public void DependencyInjection_Success()
    {
      var services = new ServiceCollection();

      services.AddSingleton<ITestOutputHelper>(_output);
      services.AddScoped<ILogger>(f => f.GetService<ITestOutputHelper>().BuildLogger());
      services.AddScoped<ILogger<RmqDispatcher>>(f => 
        f.GetService<ITestOutputHelper>().BuildLoggerFor<RmqDispatcher>());
      services.AddSingleton<IQueueMessageMapperRegistry>((s) => new Mock<IQueueMessageMapperRegistry>().Object);
      services.AddSingleton<IQueueMessageMapperFactory>((s => new Mock<IQueueMessageMapperFactory>().Object));
      services.AddRmqQueueing(new RmqConnectionDetails{ });

      var provider = services.BuildServiceProvider();
      var dispatcher = provider.GetService<RmqDispatcher>();

      Assert.NotNull(dispatcher);
    }
  }
}