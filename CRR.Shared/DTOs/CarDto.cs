namespace CRR.Shared.DTOs;

public class CarDto
{
    public Guid Id { get; set; }
    public string PlateNumber { get; set; }
    public bool Default { get; set; }

}

public class CreateCarDto
{
    public string PlateNumber { get; set; }
    public bool Default { get; set; }
}
