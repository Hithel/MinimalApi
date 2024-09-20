using AutoMapper;
using FluentValidation;
using MinimalApi.Models.Dtos.User;
using MinimalApi.Models.Entities;
using MinimalApi.Models.ViewModels.User;
using MinimalApi.Repository.Interfaces;
using MinimalApi.Services.IServices;

namespace MinimalApi.Services.Implementations;

public class UserService : GenericService<UserVm, UserDto, User>, IUserService
{
    private readonly IUser _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UserDto> _validator;

    public UserService(IUser userRepository, IMapper mapper, IValidator<UserDto> validator)
        : base(userRepository, mapper, validator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<UserVm> GetByUsernameAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        return _mapper.Map<UserVm>(user);
    }

    public async Task<UserVm> GetByRefreshTokenAsync(string Username)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(Username);
        return _mapper.Map<UserVm>(user);  
    }




}
