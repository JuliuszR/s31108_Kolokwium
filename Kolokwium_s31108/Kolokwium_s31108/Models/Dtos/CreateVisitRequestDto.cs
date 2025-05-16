namespace Kolokwium_s31108.Models.Dtos;

public class CreateVisitRequestDto
{
    public int VisitId { get; set; }
    public int ClientId { get; set; }
    //public string MechanicLicenceNumber { get; set; } = String.Empty;
    public List<ServiceInputDto> Services { get; set; } = new();
}

public class ServiceInputDto
{
    public string ServiceName { get; set; } = String.Empty;
    //public decimal ServiceFee { get; set; }
}