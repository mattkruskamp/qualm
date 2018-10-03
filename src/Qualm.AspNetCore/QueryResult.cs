namespace Qualm.AspNetCore
{
    public class QueryResult : ApiResult
    {
        public QueryResult(object data) : this(200, data)
        {

        }

        public QueryResult(int statusCode, object data) : base(statusCode)
        {
            Data = data;
        }

        /// <summary>
        /// The data object returned by the executed query
        /// </summary>
        public object Data { get; set; }
    }
}
