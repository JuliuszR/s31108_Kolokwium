namespace Kolokwium_s31108.Models.Dtos;

public class VisitDetailsDto
{
    public DateTime Date { get; set; }
    public ClientDetailsDto? Client { get; set; }
    public MechanicDetailsDto? Mechanic { get; set; }
    public List<VisitServicesDto> VisitServices { get; set; } = [];
}

public class ClientDetailsDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public DateTime DateOfBirth { get; set; }
}

public class MechanicDetailsDto
{
    public int MechanicId { get; set; }
    public string LicenceNumber { get; set; } = String.Empty;
}

public class VisitServicesDto
{
    public string Name { get; set; } = String.Empty;
    public decimal ServiceFee { get; set; }
}