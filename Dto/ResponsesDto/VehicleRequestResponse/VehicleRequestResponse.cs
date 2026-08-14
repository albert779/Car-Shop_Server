namespace CarsShop.Dto.Responses.VehicleRequests
{
    public class VehicleRequestResponse
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }
        public string Vehicle { get; set; } = string.Empty;
        public string? Model { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Color { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime RequestedOn { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}