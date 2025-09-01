namespace CRR.Shared.DTOs;

public class DefaultTripDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = "Business";
    public Guid FromId { get; set; }
    public Guid ToId { get; set; }
    public int DefaultMileage { get; set; }
    public int PrivateMileage { get; set; }
    public TimeSpan TripTime { get; set; }
}

public class CreateDefaultTripDto
{
    public string Type { get; set; } = "Business";
    public Guid FromId { get; set; }
    public Guid ToId { get; set; }
    public int DefaultMileage { get; set; }
    public int PrivateMileage { get; set; }
    public TimeSpan TripTime { get; set; }
}
