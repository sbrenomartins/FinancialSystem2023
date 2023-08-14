using Domain.Interfaces.FinancialSystems;
using Domain.Interfaces.Services;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FinancialSystemController : ControllerBase
{
    private readonly IFinancialSystem _repository;
    private readonly IFinancialSystemService _service;

    public FinancialSystemController(IFinancialSystem repository, IFinancialSystemService service)
    {
        _repository = repository;
        _service = service;
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Read()
    {
        try
        {
            var financialSystems = await _repository.ReadAll();
            return Ok(financialSystems);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> ReadById(int id)
    {
        try
        {
            var financialSystem = await _repository.ReadById(id);
            return Ok(financialSystem);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{userEmail}")]
    [Produces("application/json")]
    public async Task<IActionResult> ReadUserFinancialSystemsByEmail(string userEmail)
    {
        try
        {
            var userFinancialSystems = await _repository.GetUserFinancialSystemsByEmail(userEmail);
            return Ok(userFinancialSystems);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> Create([FromBody] FinancialSystem financialSystem)
    {
        try
        {
            await _service.AddFinancialSystem(financialSystem);
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> Update(int id, [FromBody] FinancialSystem financialSystem)
    {
        try
        {
            await _service.UpdateFinancialSystem(financialSystem);
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Produces("application/user")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var financialSystem = await _repository.ReadById(id);

            if (financialSystem == null)
                return NotFound();

            await _repository.Delete(financialSystem);
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
