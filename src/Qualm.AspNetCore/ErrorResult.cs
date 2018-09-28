namespace Qualm.AspNetCore
{
    public class ErrorResult : ApiResult
    {
        public ErrorResult(int statusCode, 
            string code, string message) : base(statusCode)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// A friendly error specific code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// A brief description of the error
        /// </summary>
        public string Message { get; set; }
    }
}
