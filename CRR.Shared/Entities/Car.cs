using System.ComponentModel.DataAnnotations.Schema;

namespace CRR.Shared.Entities;

public class Car
{
	public Guid Id { get; set; }
	public string PlateNumber { get; set; }
	public bool Default { get; set; } = false;
	public bool Deleted {get; set;}
}