namespace Qualm.AspNet
{
    public class ApiResult
    {
        public ApiResult(int statusCode)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// The Http Response status code
        /// </summary>
        public int StatusCode { get; set; }
    }
}
