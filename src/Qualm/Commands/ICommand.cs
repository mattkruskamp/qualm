using System;

namespace Qualm.Commands
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}
