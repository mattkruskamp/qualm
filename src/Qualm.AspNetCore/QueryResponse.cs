namespace Qualm.AspNetCore.Queries
{
    /// <summary>
    /// An api response for a query 
    /// </summary>
    public class QueryResponse : ApiResponse
    {
        public QueryResponse()
        {
            Status = "success";
            StatusCode = 200;
        }
    }
}
