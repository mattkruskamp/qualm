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
            IEnumerable<ErrorResponse> errors) : 
            base(400, "validation_error", "There was an error validating the object.")
        {   
            Errors = errors;
        }

        /// <summary>
        /// A collection of validation errors
        /// </summary>
        public IEnumerable<ErrorResponse> Errors { get; set; }
    }
}
