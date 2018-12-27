using System;
using System.Collections.Generic;
using System.Text;

namespace Qualm.Queuing
{
    public interface IQueueMessageMapperRegistry
    {
        Type GetMapper(Type requestType);

        void RegisterMapper(Type requestType, Type messageMapperType);


        // Could do something like:
        // GetMapper(IRequest) where bleh bleh and
        // GetMapper(QueueMessage) where bleh bleh bleh
    }
}
