using Newtonsoft.Json;

namespace Qualm.Queuing.Json
{
    public abstract class JsonQueueMessageMapper<TRequest>
        : IQueueMessageMapper<TRequest> where TRequest : class, IRequest
    {
        public QueueMessage ToMessage(TRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var queueMessage = new QueueMessage()
            {
                Subject = this.Subject,
                Body = json
            };
            return queueMessage;
        }

        public TRequest ToRequest(QueueMessage message)
        {
            var result = JsonConvert.DeserializeObject(
                message.Body!, typeof(TRequest));
            return (result as TRequest)!;
        }

        public abstract string Subject { get; }
    }
}
