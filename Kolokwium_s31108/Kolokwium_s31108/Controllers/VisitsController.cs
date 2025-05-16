using Kolokwium_s31108.Exceptions;
using Kolokwium_s31108.Models.Dtos;
using Kolokwium_s31108.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium_s31108.Controllers;


[Route("api/[controller]")]
[ApiController]
public class VisitsController : ControllerBase
{
    private readonly IDbServices _dbServices;

    public VisitsController(IDbServices dbServices)
    {
        _dbServices = dbServices;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVisitAsync(int id)
    {
        try
        {
            var res = await _dbServices.GetVisitDetailsAsync(id);
            return Ok(res);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddVisitAsync(CreateVisitRequestDto createVisitRequestDto)
    {
        if (!createVisitRequestDto.Services.Any())
        {
            return BadRequest("At least one item is needed");
        }

        try
        {
            await _dbServices.AddNewVisitAsync(createVisitRequestDto);
            return Created("", null);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return   BadRequest(e.Message);
        }
    }
}