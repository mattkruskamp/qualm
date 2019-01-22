namespace Qualm.Queuing
{
    public interface IQueueClientFactory
    {
        IQueueClient Create();
    }
}
