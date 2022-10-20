using RabbitMQ.Client;
using System.Collections.Generic;

namespace Qualm.Rmq
{
    public class RmqChannelFactory
    {
        readonly RmqConnectionFactory _mananger;
        readonly IList<string> _initializedQueues;
        readonly RmqConnectionDetails _connectionDetails;
        private IModel _channel;

        public RmqChannelFactory(
            RmqConnectionFactory manager,
            RmqConnectionDetails connectionDetails)
        {
            _mananger = manager;
            _connectionDetails = connectionDetails;
            _initializedQueues = new List<string>();
        }

        public IModel Create(string queueName)
        {
            if (_channel == null || _channel.IsClosed)
                _channel = BuildChannel();
            InitializeChannel(_channel, queueName);
            return _channel;
        }

        protected virtual IModel BuildChannel()
        {
            var channel = _mananger.Create().CreateModel();
            return channel;
        }

        protected virtual void InitializeChannel(
            IModel channel, string queueName)
        {
            if (_initializedQueues.Count < 1)
                channel.ExchangeDeclare(_connectionDetails.Exchange, "direct", true, false, null);

            if (_initializedQueues.Contains(queueName))
                return;

            channel.QueueDeclare(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
    }
}
