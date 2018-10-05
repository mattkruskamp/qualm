namespace Qualm.AspNetCore
{
    /// <summary>
    /// A general class for basic api response info
    /// </summary>
    public class ApiResponse
    {
        public ApiResponse()
        {

        }

        public ApiResponse(string status, int statusCode)
        {
            Status = status;
            StatusCode = statusCode;
        }

        /// <summary>
        /// A string representation of the response status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The Http status code for the response
        /// </summary>
        public int StatusCode { get; set; }
    }
}
