public class CarInfoRequest
{
    public int Id { get; set; }
    public int CarId { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Details { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}