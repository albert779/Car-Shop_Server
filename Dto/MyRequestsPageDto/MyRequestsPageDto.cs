using CarsShop.Models.Responses;

namespace CarsShop.Dto.RequestsDto
{
    public class MyRequestsPageDto
    {
        public List<TableOfRequests> Items { get; set; } = new();

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }
    }
}