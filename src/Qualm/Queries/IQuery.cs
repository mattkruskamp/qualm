namespace Qualm.Queries
{
    public interface IQuery
    {
    }

    public interface IQuery<out T> : IQuery
    {
    }
}
