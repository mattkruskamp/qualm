using Qualm.Commands;
using System;

namespace Qualm.Tests.Commands
{
    public class TestCommand : ICommand
    {
        public TestCommand()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public int CommandNumber { get; set; }
    }
}
