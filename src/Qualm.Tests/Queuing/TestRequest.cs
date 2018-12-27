using System;
using System.Collections.Generic;
using System.Text;

namespace Qualm.Tests.Queuing
{
    public class TestRequest : IRequest
    {
        private readonly Guid _id;

        public TestRequest()
        {
            _id = Guid.NewGuid();
        }

        public Guid Id
        {
            get { return _id; }
        }
    }
}
