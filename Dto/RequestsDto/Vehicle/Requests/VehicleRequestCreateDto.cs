public class VehicleRequestCreateDto
{
    public int CarId { get; set; }
    public string Message { get; set; } = string.Empty;



    public static CarsShop.Dto.Responses.Requests.VehicleRequestCreateDto ConvertServiceDto(VehicleRequestCreateDto request)
    {
        var item = new CarsShop.Dto.Responses.Requests.VehicleRequestCreateDto()
        {
            CarId = request.CarId,
            Message = request.Message,
        };
        return item;
    }
}