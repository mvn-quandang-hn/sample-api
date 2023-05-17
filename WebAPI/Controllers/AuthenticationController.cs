using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.DTOs;
using WebAPI.Application.Services.Interfaces;
using WebAPI.Common;

namespace WebAPI.Controllers;

[ApiController]
[Route($"{Constants.ApiPrefix}/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    public IConfiguration Configuration;

    public AuthenticationController(IAuthenticationService authenticationService, IConfiguration config)
    {
        _authenticationService = authenticationService;
        Configuration = config;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<AuthenticationResponse>>> GetJwtToken()
    {
        //TODO: Check login

        var secretKey = Configuration["Jwt:SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
        {
            ModelState.AddModelError(nameof(secretKey), ErrorMessage.MissingRequiredElement);
            return ValidationProblem(ModelState);
        }
        return Ok(new BaseResponse<AuthenticationResponse>
        {
            Data = await _authenticationService.GetJwtToken(secretKey)
        });
    }
}