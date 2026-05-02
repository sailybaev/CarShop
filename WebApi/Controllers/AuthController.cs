using CarShopFinal.Persistance.Dependency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShopFinal.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]

public class AuthController:ControllerBase
{
    private readonly AuthService _authService;
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] string email,[FromForm] string password, [FromForm] string role)
    {
        var token = await _authService.RegisterUser(email, password,role);
        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
    {
        var token = await _authService.LoginUser(email, password);
        return Ok(token);
    }
    [Authorize]
    [HttpGet("secure")]
    public IActionResult Secure()
    {
        return Ok("Ты авторизован");
    }
    
}