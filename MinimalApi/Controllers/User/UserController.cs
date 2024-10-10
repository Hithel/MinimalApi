using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Helpers.Services;
using MinimalApi.Helpers.TwoStepAuth;
using MinimalApi.Models.Dtos.Authentication;
using MinimalApi.Models.Dtos.User;
using MinimalApi.Models.ViewModels.Authentication;
using MinimalApi.Models.ViewModels.User;
using MinimalApi.Services.Implementations;
using MinimalApi.Services.IServices;


namespace MinimalApi.Controllers.User;

public class UserController : ApiBaseController
{
    private readonly IUserService _service;
    private readonly IUserAuthenticationService _userAuthenticationService;

    public UserController(IUserService service, IUserAuthenticationService userAuthenticationService)
    {
        _service = service;
        _userAuthenticationService = userAuthenticationService;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get()
    {
        var entidades = await _service.GetAllAsync();
        return Ok(entidades);
    }



    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult> RegisterAsync(RegisterDto model)
    {
        var result = await _userAuthenticationService.RegisterAsync(model);
        return Ok(result);
    }



    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Login([FromBody] LoginDto data)
    {
        try
        {
            // Llamamos al servicio para ejecutar la lógica
            DataUserDto tokenData = await _userAuthenticationService.LoginAsync(data);

            // Aquí simplemente se establece la cookie
            SetRefreshTokenInCookie(tokenData.RefreshToken!);

            return Ok(tokenData);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpPost("VerifyCode")]
    public async Task<ActionResult> Verify ([FromBody] AuthDto dto)
    {
        try
        {
            var Isverified = await _userAuthenticationService.VerifyAsync(dto);

            if (Isverified)
            {
                return Ok("authenticated, checked");
            }
            return Unauthorized();
        }
        catch (Exception ex)
        {
            return BadRequest($"error, some error occurred: {ex}");
        }
    }



    private void SetRefreshTokenInCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(10),
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }


}
