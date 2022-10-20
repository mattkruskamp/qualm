using RabbitMQ.Client;
using System;

namespace Qualm.Rmq
{
    public class RmqConnectionFactory
    {
        readonly RmqConnectionDetails _settings;
        private IConnection connection;

        public RmqConnectionFactory(RmqConnectionDetails settings)
        {
            _settings = settings;
        }

        protected virtual IConnection BuildConnection()
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_settings.Hostname)
            };
            var connection = factory.CreateConnection();
            return connection;
        }

        public IConnection Create()
        {
            if (connection == null)
                connection = BuildConnection();
            return connection;
        }
    }
}
