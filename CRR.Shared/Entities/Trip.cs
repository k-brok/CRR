namespace CRR.Shared.Entities;

public class Trip{
	public Guid Id {get; set;}
	public Address From {get; set;}
	public Guid FromId {get; set;}
	public Address To {get; set;}
	public Guid ToId {get; set;}
	public string Remark {get; set;}
	public int DepartureMileage {get; set;}
	public int ArrivalMileage {get; set;}
	public DateTime Departure {get; set;}
	public DateTime Arrival {get; set;}
	public Distance Distance {get; set;}
}