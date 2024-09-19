using AutoMapper;
using MinimalApi.Models.Dtos.Rol;
using MinimalApi.Models.Dtos.User;
using MinimalApi.Models.Entities;
using MinimalApi.Models.ViewModels.User;

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