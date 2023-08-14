using System.Globalization;
using System.Text;
using Api.DTOs.RequestModels;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Api;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserController(UserManager<ApplicationUser> userManager,
                           SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestModel request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password) ||
            string.IsNullOrWhiteSpace(request.Cpf))
            return BadRequest("All fields are obrigatory");

        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email,
            Cpf = request.Cpf
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Errors.Any())
            return BadRequest(result.Errors);

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

        var response = await _userManager.ConfirmEmailAsync(user, code);

        if (response.Succeeded)
            return NoContent();

        return BadRequest("Error ocurred when try to add user");
    }
}
