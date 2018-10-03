namespace Qualm.AspNetCore
{
    public class CommandResult : ApiResult
    {
        public CommandResult(object data) : this(200, data)
        {

        }

        public CommandResult(int statusCode, object data) : base(statusCode)
        {
            Data = data;
        }

        /// <summary>
        /// The data object being returned from the executed command
        /// </summary>
        public object Data { get; set; }
    }
}
