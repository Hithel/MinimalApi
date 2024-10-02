using AutoMapper;
using MinimalApi.Models.Dtos.Rol;
using MinimalApi.Models.Dtos.User;
using MinimalApi.Models.Entities;
using MinimalApi.Models.ViewModels.Rol;
using MinimalApi.Models.ViewModels.User;

namespace MinimalApi.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<User, UserVm>().ReverseMap();

        CreateMap<RolDto, Rol>().ReverseMap();
        CreateMap<Rol, RolVm>().ReverseMap();

    }
}