namespace CRR.Shared.DTOs;

public class TripDto
{
    public Guid Id { get; set; }
    public Guid FromId { get; set; }
    public Guid ToId { get; set; }
    public string Remark {get; set;}
    public int DepartureMileage {get; set;}
	public int ArrivalMileage {get; set;}
    public DateTime Departure { get; set; }
    public DateTime Arrival { get; set; }
    public DistanceDto Distance { get; set; }
}

public class CreateTripDto
{
    public Guid FromId { get; set; }
    public Guid ToId { get; set; }
    public string Remark {get; set;}
    public int DepartureMileage {get; set;}
	public int ArrivalMileage {get; set;}
    public DateTime Departure { get; set; }
    public DateTime Arrival { get; set; }
    public DistanceDto Distance { get; set; }
}
