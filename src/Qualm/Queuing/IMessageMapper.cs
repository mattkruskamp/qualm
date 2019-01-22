namespace Qualm.Queuing
{
    public interface IQueueMessageMapper { }

    public interface IQueueMessageMapper<TRequest> : IQueueMessageMapper
        where TRequest : class, IRequest
    {
        QueueMessage ToMessage(TRequest request);

        TRequest ToRequest(QueueMessage message);
    }
}
