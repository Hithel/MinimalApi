using AutoMapper;
using MinimalApi.Dtos.Rol;
using MinimalApi.Dtos.User;
using MinimalApi.Models;
using MinimalApi.ViewModels.User;

namespace MinimalApi.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<UserDto, User>();
        CreateMap<User, UserVm>();

        CreateMap<RolDto, Rol>();
        CreateMap<User, UserVm>();

    }
}