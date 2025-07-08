namespace CRR.API.DTOs;

public class AddressDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Street { get; set; }
    public string Number { get; set; }
    public string ZipCode { get; set; }
    public string Type { get; set; } = "Business";
}

public class CreateAddressDto
{
    public string Name { get; set; } = string.Empty;
    public string Street { get; set; }
    public string Number { get; set; }
    public string ZipCode { get; set; }
    public string Type { get; set; } = "Business";
}

