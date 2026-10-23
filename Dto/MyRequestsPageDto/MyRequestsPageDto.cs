using TableOfRequestsDto =
    CarsShop.Dto.RequestsDto.TableOfRequests.TableOfRequests;

namespace CarsShop.Dto.RequestsDto
{
    public class MyRequestsPageDto
    {
        public List<TableOfRequestsDto> Items { get; set; } = new();

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }
    }
}