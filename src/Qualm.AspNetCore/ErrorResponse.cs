namespace Qualm.AspNetCore
{
    /// <summary>
    /// A general class providing basic error info
    /// </summary>
    public class ErrorResponse : ApiResponse
    {
        public ErrorResponse()
        {

        }

        public ErrorResponse(
            string status, int statusCode,
            string errorCode, string errorMessage)
        {
            Status = status;
            StatusCode = statusCode;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// A friendly error specific code
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// A brief description of the error
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
