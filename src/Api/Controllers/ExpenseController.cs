using Domain.Interfaces.Expenses;
using Domain.Interfaces.Services;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ExpenseController : ControllerBase
{
    private readonly IExpense _repository;
    private readonly IExpenseService _service;

    public ExpenseController(IExpense repository, IExpenseService service)
    {
        _repository = repository;
        _service = service;
    }

    [HttpGet("{userEmail}")]
    [Produces("application/json")]
    public async Task<IActionResult> ReadByEmail(string userEmail)
    {
        try
        {
            var expenses = await _repository.GetUserExpensesByEmail(userEmail);
            return Ok(expenses);
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
            var expense = await _repository.ReadById(id);
            return Ok(expense);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{userEmail}/not-paid")]
    [Produces("application/json")]
    public async Task<IActionResult> ReadUserNotPaidByEmail(string userEmail)
    {
        try
        {
            var expenses = await _repository.GetUserNotPaidLastMonthsExpensesByEmail(userEmail);
            return Ok(expenses);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> Create(Expense expense)
    {
        try
        {
            await _service.AddExpense(expense);
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> Update(int id, Expense expense)
    {
        try
        {
            await _service.UpdateExpense(expense);
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
            var expense = await _repository.ReadById(id);

            if (expense == null)
                return NotFound();

            await _repository.Delete(expense);
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
