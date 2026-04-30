using CarsShop.Db.Models;

public class VehicleRequestCreateDto
{
    public int CarId { get; set; }
    public string Details { get; set; } = string.Empty;


    public static VehicleRequest ConvertToDbModel(VehicleRequestCreateDto request, RequestStatus status, int userId, DateTime createdAt, DateTime lastUpdate, RequestStatus requestStatus, VehicleType vehicleType)
    {
        var vehicle = new Vehicle() { Id = request.CarId, VehicleType = vehicleType };
        var user = new User() { Id = userId };
        var dbModel = new VehicleRequest()
        {
            Vehicle = vehicle,
            Message = request.Details,
            Status = requestStatus,
            CreatedAt = createdAt,
            LastUpdate = lastUpdate,
            User = user,
        };

        return dbModel;
    }
}