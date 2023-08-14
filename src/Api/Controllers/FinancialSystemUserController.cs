using Api.DTOs;
using Domain.Interfaces.FinancialSystemUsers;
using Domain.Interfaces.Services;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FinancialSystemUserController : ControllerBase
{
    private readonly IFinancialSystemUser _repository;
    private readonly IFinancialSystemUserService _service;

    public FinancialSystemUserController(IFinancialSystemUser repository, IFinancialSystemUserService service)
    {
        _repository = repository;
        _service = service;
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> ReadFinancialSystemUsersById(int id)
    {
        try
        {
            var users = await _repository.GetFinancialSystemUsersById(id);
            return Ok(users);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> Create([FromBody] CreateFinancialSystemUserRequestModel request)
    {
        try
        {
            var financialSystemUser = new FinancialSystemUser
            {
                ActualSystem = true,
                Administrator = false,
                SystemId = request.SystemId,
                UserEmail = request.UserEmail
            };
            await _service.AddFinancialSystemUser(financialSystemUser);
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var financialSystemUser = await _repository.ReadById(id);

            if (financialSystemUser == null)
                return NotFound();

            await _repository.Delete(financialSystemUser);
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
