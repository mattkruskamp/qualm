using Qualm.Queuing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qualm.Tests.Queuing
{
    public class TestRequestMapper : IQueueMessageMapper<TestRequest>
    {
        public QueueMessage ToMessage(TestRequest request)
        {
            return new QueueMessage();
        }

        public TestRequest ToRequest(QueueMessage message)
        {
            return new TestRequest();
        }
    }
}
