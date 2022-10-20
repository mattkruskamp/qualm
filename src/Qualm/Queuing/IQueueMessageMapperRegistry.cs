using System;
using System.Reflection;

namespace Qualm.Queuing
{
    public interface IQueueMessageMapperRegistry
    {
        Type? GetMapper(Type requestType);

        void RegisterMapper(Type requestType, Type mapperType);

        void RegisterMappers(params Assembly[] assemblies);
    }
}
