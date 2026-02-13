using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Planner.API.Models;
using Planner.Core.Interfaces;
using Planner.Infrastructure.Entities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using RegisterRequest = Planner.API.Models.RegisterRequest;

namespace Planner.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuthController(
    UserManager<ApplicationUser> userManager, 
    IJwtService jwtService,
    ILogger<AuthController> logger): ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser {UserName = request.UserName, Email = request.Email,};
        var result = await userManager.CreateAsync(user, request.Password);
        
        if(!result.Succeeded)
        {
            logger.LogWarning("Registration failed for email {Email}: {Errors}",
                request.Email, string.Join("; ", result.Errors.Select(e => e.Description)));
            return BadRequest(result.Errors);
        }
        
        logger.LogInformation("User registered. UserId={UserId}, Email={Email}", user.Id, user.Email);
        return Created(string.Empty, new { id = user.Id });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            logger.LogWarning("Login failed: user not found for email {Email}", request.Email);
            return Unauthorized();
        }

        var isValid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!isValid)
        {
            logger.LogWarning("Login failed: invalid password for email {Email}", request.Email);
            return Unauthorized();
        }

        logger.LogInformation("User logged in. UserId={UserId}, Email={Email}", user.Id, user.Email);
        var token = jwtService.GenerateToken(user.Id, user.Email ?? "");
        return Ok(new LoginResponse(token));
    }
    
    [AllowAnonymous]
    [HttpGet("google")]
    public IActionResult GoogleLogin()
    {
        var redirectUrl = Url.Action(nameof(GoogleCallback), "Auth");
        var properties = new AuthenticationProperties {RedirectUri = redirectUrl};
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [AllowAnonymous]
    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback(CancellationToken cancellationToken)
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (!result.Succeeded)
            return Unauthorized();
        
        var email = result.Principal?.FindFirst(ClaimTypes.Email)?.Value;
        var name = result.Principal?.FindFirst(ClaimTypes.Name)?.Value;
        
        if(string.IsNullOrEmpty(email))
            return BadRequest("Email not received from Google");
        
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = email.Split("@").First(),
                Email = email,
            };
            await userManager.CreateAsync(user);
        }
        
        var token = jwtService.GenerateToken(user.Id, user.Email ?? "");
        
        logger.LogInformation("User logged in via Google. UserId={UserId}, Email={Email}", user.Id, user.Email);
        
        return Ok(new LoginResponse(token));
    }
}