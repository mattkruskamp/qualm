using System;

namespace Qualm.AspNetCore.Commands
{
    /// <summary>
    /// An api response for a command
    /// </summary>
    public class CommandResponse : ApiResponse
    {
        public CommandResponse(Guid commandId, string status)
        {
            Id = commandId;
            Status = status;
            StatusCode = 200;
        }

        /// <summary>
        /// A unique identifier for the command providing this response
        /// </summary>
        public Guid Id { get; set; }
    }
}
