namespace Awarean.Operational.Cutler.Application.Queries
{
    public interface IPaginatedQuery
    {
        public int PageSize { get; set; }
        public string NextPageToken { get; set; }
    }
}