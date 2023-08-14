using Domain.Interfaces.Categories;
using Domain.Interfaces.Services;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategory _repository;
    private readonly ICategoryService _service;

    public CategoryController(ICategory repository, ICategoryService service)
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
            var categories = await _repository.GetUserCategoriesByEmail(userEmail);
            return Ok(categories);
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
            var category = await _repository.ReadById(id);
            return Ok(category);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<IActionResult> Create(Category category)
    {
        try
        {
            await _service.AddCategory(category);
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    public async Task<IActionResult> Update(int id, Category category)
    {
        try
        {
            await _service.UpdateCategory(category);
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
            var category = await _repository.ReadById(id);

            if (category == null)
                return NotFound();

            await _repository.Delete(category);
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
