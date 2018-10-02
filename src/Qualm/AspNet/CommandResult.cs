namespace Qualm.AspNet
{
    public class CommandResult : ApiResult
    {
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
