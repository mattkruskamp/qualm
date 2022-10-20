using System;
using System.Collections.Generic;
using System.Text;

namespace Qualm.Tests.Queuing
{
    public class TestRequest : IRequest
    {
        readonly Guid _id;

        public TestRequest() =>
            _id = Guid.NewGuid();

        public Guid Id
        {
            get => _id;
        }
    }
}