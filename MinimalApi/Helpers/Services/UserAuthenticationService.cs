using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MinimalApi.Helpers.Authentication.Security;
using MinimalApi.Models.Dtos.Authentication;
using MinimalApi.Models.Dtos.User;
using MinimalApi.Models.Entities;
using MinimalApi.Models.ViewModels.Authentication;
using MinimalApi.Repository.Interfaces;
using MinimalApi.Services.IServices;
using System.IdentityModel.Tokens.Jwt;

namespace MinimalApi.Helpers.Services;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserService _userService; // Servicio para usuarios
    private readonly IRolService _rolService; // Servicio para roles
    private readonly JwtSettings _jwtSettings;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public UserAuthenticationService(IPasswordHasher<User> passwordHasher, IUserService userService, IRolService rolService, JwtSettings jwtSettings, IMapper mapper, ITokenService tokenService)
    {
        _passwordHasher = passwordHasher;
        _userService = userService;
        _rolService = rolService;
        _jwtSettings = jwtSettings;
        _mapper = mapper;
        _tokenService = tokenService;
    }


    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var user = new User
        {
            Email = registerDto.Email,
            Username = registerDto.Username,
            DateCreated = registerDto.DateCreated
        };

        user.Password = _passwordHasher.HashPassword(user, registerDto.Password);

        var existingUser = _userService.GetByUsernameAsync(registerDto.Username);


        if (existingUser == null)
        {
            var rolDefaultVm = _rolService.GetRolNameByNameAsync(registerDto.Username);
            var rolDefault = _mapper.Map<Rol>(rolDefaultVm);

            try
            {
                user.Rols.Add(rolDefault);
                var userDto = _mapper.Map<UserDto>(user);
                await _userService.AddAsync(userDto);

                return $"User  {registerDto.Username} has been registered successfully";
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return $"Error: {message}";
            }
        }
        else
        {
            return $"User {registerDto.Username} already registered.";
        }
    }

    public async Task<DataUserDto> GetTokenAsync(LoginDto model)
    {
        DataUserDto dataUserDto = new DataUserDto();

        var userVm = await _userService.GetByUsernameAsync(model.Username);
        var userEntity = _mapper.Map<User>(userVm);

        if (userEntity == null)
        {
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = $"User does not exist with username: {model.Username}";
            return dataUserDto;
        }

        if (userEntity.Password == null)
        {
            // Manejar el caso donde la contraseña no está disponible
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = "Password is not set.";
            return dataUserDto;
        }

        var result = _passwordHasher.VerifyHashedPassword(userEntity, userEntity.Password, model.Password);

        if (result == PasswordVerificationResult.Success)
        {
            dataUserDto.Message = "EL CODIGO QR SE HA ENVIADO AL EMAIL!! <3";
            dataUserDto.IsAuthenticated = true;
            dataUserDto.Token = _tokenService.GenerateJwtToken(userEntity);
            dataUserDto.Email = userEntity.Email!;
            dataUserDto.UserName = userEntity.Username!;
            dataUserDto.Roles = userEntity.Rols
                                            .Select(u => u.Nombre)
                                            .ToList();

            if (userEntity.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = userEntity.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                dataUserDto.RefreshToken = activeRefreshToken.Token;
                dataUserDto.RefreshTokenExpiration = activeRefreshToken.Expires;
            }

            else
            {
                var refreshToken = CreateRefreshToken();
                dataUserDto.RefreshToken = refreshToken.Token;
                dataUserDto.RefreshTokenExpiration = refreshToken.Expires;
                userEntity.RefreshTokens.Add(refreshToken);
            
                var userDto = _mapper.Map<UserDto>(userEntity);
                await _userService.UpdateAsync(userDto,   userEntity.Id);
            }

            return dataUserDto;
        }



    }


}
