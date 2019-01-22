using Qualm.Queuing;
using System;

namespace Qualm.Rmq
{
    public class RmqQueueClientFactory : IQueueClientFactory
    {
        private readonly IServiceProvider _provider;

        public RmqQueueClientFactory(
            IServiceProvider provider)
        {
            _provider = provider;
        }

        public IQueueClient Create()
        {
            return (RmqQueueClient)_provider.GetService(typeof(RmqQueueClient));
        }
    }
}
