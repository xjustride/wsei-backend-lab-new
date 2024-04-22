using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using Infrastructure.EF;
using Infrastructure.EF.Entities;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Web.Configuration;

namespace WebAPI.Controllers;

[ApiController, Route("/api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<UserEntity> _manager;
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<AuthenticationController> _logger;

    // Połącz wszystkie zależności w jednym konstruktorze
    public AuthenticationController(
        UserManager<UserEntity> manager, 
        JwtSettings jwtSettings, 
        ILogger<AuthenticationController> logger)
    {
        _manager = manager;
        _jwtSettings = jwtSettings;
        _logger = logger;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] LoginUserDto user)
    {
        _logger.LogInformation("Authentication attempt for user: {LoginName}", user.LoginName);

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Login failed: Model state is invalid for user: {LoginName}", user.LoginName);
            return Unauthorized();
        }

        var logged = await _manager.FindByNameAsync(user.LoginName);
        if (logged != null && await _manager.CheckPasswordAsync(logged, user.Password))
        {
            _logger.LogInformation("Login successful for user: {LoginName}", user.LoginName);
            return Ok(new {Token = CreateToken(logged)});
        }

        _logger.LogWarning("Login failed: Invalid credentials for user: {LoginName}", user.LoginName);
        return Unauthorized();
    }

    private string CreateToken(UserEntity user)
    {
        return new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            .AddClaim(JwtRegisteredClaimNames.Name, user.UserName)
            .AddClaim(JwtRegisteredClaimNames.Gender, "male")
            .AddClaim(JwtRegisteredClaimNames.Email, user.Email)
            .AddClaim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds())
            .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid())
            .Audience(_jwtSettings.Audience)
            .Issuer(_jwtSettings.Issuer)
            .Encode();
    }
}