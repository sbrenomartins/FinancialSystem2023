using Api.DTOs.RequestModels;
using Api.Token;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public TokenController(UserManager<ApplicationUser> userManager,
                           SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [Produces("application/json")]
    [HttpPost("CreateToken")]
    public async Task<IActionResult> CreateToken([FromBody] CreateTokenRequestModel request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            return Unauthorized();

        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
        if (result.Succeeded)
        {
            var token = new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Create("Secret_Key_12345678"))
                .AddSubject("FinancialSystem2023")
                .AddIssuer("Test.Security.Bearer")
                .AddAudience("Test.Security.Bearer")
                .AddClaim("User", "1")
                .AddExpiry(5)
                .Builder();

            return Ok(token.value);
        }

        return Unauthorized();
    }
}
