using System.Collections.Generic;

namespace Qualm.AspNetCore
{
    public class ValidationErrorResult : ErrorResult
    {
        public ValidationErrorResult(
            int statusCode, 
            string code, string message, 
            ErrorResult[] errors) : base(statusCode, code, message)
        {
            Errors = errors;
        }

        /// <summary>
        /// A collection of validation errors
        /// </summary>
        public IEnumerable<ErrorResult> Errors { get; set; }
    }
}
