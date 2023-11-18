namespace ProjectStrayToHomeAPI.Models.DTO
{
    public class PaginatorResultDTO<T>
    {
        public List<T> Data { get; set; } = null!;
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public PaginatorResultDTO(List<T> data, int pageIndex, int pageSize, int totalCount)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
        }
    }
}
