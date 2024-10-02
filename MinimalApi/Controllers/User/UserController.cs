using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Helpers.Services;
using MinimalApi.Helpers.TwoStepAuth;
using MinimalApi.Models.Dtos.Authentication;
using MinimalApi.Models.Dtos.User;
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

}
