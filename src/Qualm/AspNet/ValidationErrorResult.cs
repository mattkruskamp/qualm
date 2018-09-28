using System.Collections.Generic;

namespace Qualm.AspNet
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
