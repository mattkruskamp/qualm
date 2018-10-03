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

        /// <summary>
        /// Generates a default not found error result
        /// </summary>
        /// <returns>ErrorResult</returns>
        public static ErrorResult NotFound()
        {
            return new ErrorResult(404, "not_found",
                "The requested resource was not found.");
        }
    }
}
