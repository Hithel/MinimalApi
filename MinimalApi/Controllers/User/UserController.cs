using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Helpers.Services;
using MinimalApi.Helpers.TwoStepAuth;
using MinimalApi.Models.Dtos.User;
using MinimalApi.Models.ViewModels.User;
using MinimalApi.Services.IServices;


namespace MinimalApi.Controllers.User;

public class UserController : ApiBaseController
{
    private readonly IMapper _mapper;
    private readonly IAuth _auth;
    private readonly IPasswordHasher<UserDto> _passwordHasher;
    private readonly IUserService _service;
    private readonly IUserAuthenticationService _userAuthenticationService;

    public UserController(IMapper mapper, IAuth auth, IPasswordHasher<UserDto> passwordHasher, IUserService service, IUserAuthenticationService userAuthenticationService)
    {
        _mapper = mapper;
        _auth = auth;
        _passwordHasher = passwordHasher;
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

}
