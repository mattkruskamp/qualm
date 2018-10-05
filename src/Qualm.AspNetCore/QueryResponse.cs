namespace Qualm.AspNetCore.Queries
{
    /// <summary>
    /// An api response for a query 
    /// </summary>
    public class QueryResponse : ApiResponse
    {
        public QueryResponse(string status)
        {
            Status = status;
            StatusCode = 200;
        }
    }
}
