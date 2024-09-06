using AutoMapper;
using FluentValidation;
using MinimalApi.Dtos.User;
using MinimalApi.Models;
using MinimalApi.Repository.Interfaces;
using MinimalApi.Repository.UnitOfWork;
using MinimalApi.Services.IServices;
using MinimalApi.ViewModels.User;

namespace MinimalApi.Services.Services;

public class UserService : GenericService<UserVm, UserDto, User>
{
    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UserDto> validator)
        : base(unitOfWork, mapper, validator)
    {

    }

    protected override IGenericRepository<User> GetRepository()
    {
        return _unitOfWork.Users; // Obteniendo el repositorio de Usuarios desde UnitOfWork
    }




}
