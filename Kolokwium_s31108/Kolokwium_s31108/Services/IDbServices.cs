using Kolokwium_s31108.Models.Dtos;

namespace Kolokwium_s31108.Services;

public interface IDbServices
{
    Task<VisitDetailsDto> GetVisitDetailsAsync(int visitId);
    Task AddNewVisitAsync(CreateVisitRequestDto visit);
}