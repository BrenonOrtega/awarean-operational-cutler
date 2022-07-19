namespace Awarean.Operational.Cutler.Application.Queries
{
    public class GetFoodQuery : IPaginatedQuery
    {
        public int PageSize { get; set; }
        public string NextPageToken { get; set; }
    }
}