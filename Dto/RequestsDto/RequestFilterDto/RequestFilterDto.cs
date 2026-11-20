namespace CarsShop.Dto.RequestsDto.RequestFilterDto
{
    public class RequestFilterDto
    {
        public string? Search { get; set; }

        public int? StatusId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 5;


        public static CarsShop.Dto.Responses.Requests.RequestFilterDto ConvertToServiceDto(RequestFilterDto input)
        {
            var item = new CarsShop.Dto.Responses.Requests.RequestFilterDto()
            {
                FromDate = input.FromDate,
                ToDate = input.ToDate,
                Page = input.Page,
                PageSize = input.PageSize,
                Search = input.Search,
                StatusId = input.StatusId,
            };
            return item;
        }

    }
}