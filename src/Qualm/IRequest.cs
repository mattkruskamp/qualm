using System;
using System.Collections.Generic;
using System.Text;

namespace Qualm
{
    public interface IRequest
    {
        Guid Id { get; }
    }
}
