using CRR.Shared.Enums;

namespace CRR.Shared.Entities;
public class Address{
	public Guid Id {get; set;}
	public string Name {get; set;} = string.Empty;
	public string Street {get; set;}
	public string Number {get; set;}
	public string ZipCode {get; set;}
	public TripType Type {get; set;}
}