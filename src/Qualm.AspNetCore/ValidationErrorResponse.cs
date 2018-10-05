using System.Collections.Generic;

namespace Qualm.AspNetCore
{
    /// <summary>
    /// A class that contains validation errors
    /// </summary>
    public class ValidationErrorResponse : ErrorResponse
    {
        public ValidationErrorResponse()
        {

        }

        public ValidationErrorResponse(
            string status, int statusCode,
            string errorCode, string errorMessage,
            IEnumerable<ErrorResponse> errors) : 
            base(status, statusCode, errorCode, errorMessage)
        {   
            Errors = errors;
        }

        /// <summary>
        /// A collection of validation errors
        /// </summary>
        public IEnumerable<ErrorResponse> Errors { get; set; }
    }
}
