using CarsShop.Db.Models;
using CarsShop.Services;

public class VehicleRequestCreateDto
{
    public int CarId { get; set; }
    public string Message { get; set; } = string.Empty;


    public static VehicleRequest ConvertToDbModel(VehicleRequestCreateDto request, int userId, DateTime createdAt, DateTime lastUpdate, RequestStatusEnum status)
    {
        var vehicle = new Vehicle() { Id = request.CarId };
        var user = new User() { Id = userId };
        var statusString = status.ToString();

        var dbModel = new VehicleRequest()
        {
            Vehicle = vehicle,
            Message = request.Message,
            Status = new RequestStatus() { Name = statusString },
            CreatedAt = createdAt,
            LastUpdate = lastUpdate,
            User = user,
        };

        return dbModel;
    }
}