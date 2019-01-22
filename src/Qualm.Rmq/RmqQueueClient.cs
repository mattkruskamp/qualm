using Qualm.Queuing;
using RabbitMQ.Client;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Qualm.Rmq
{
    public class RmqQueueClient : IQueueClient
    {
        private readonly RmqChannelFactory _channels;
        private readonly RmqConnectionDetails _details;

        public RmqQueueClient(
            RmqChannelFactory channels,
            RmqConnectionDetails details)
        {
            _channels = channels;
            _details = details;
        }

        public async Task SendAsync(QueueMessage message,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var channel = _channels.Create(message.Subject);
            var body = Encoding.UTF8.GetBytes(message.Body);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            await Task.Run(() =>
            {
                channel.BasicPublish(
                    exchange: _details.Exchange,
                    routingKey: message.Subject,
                    basicProperties: properties,
                    body: body);
            });            
        }
    }
}
