using AutoMapper;
using FluentValidation;
using MinimalApi.Models.Dtos.Rol;
using MinimalApi.Models.Entities;
using MinimalApi.Models.ViewModels.Rol;
using MinimalApi.Repository.Interfaces;
using MinimalApi.Services.IServices;

namespace MinimalApi.Services.Implementations;

public class RolService : GenericService<RolVm, RolDto, Rol>, IRolService
{
    private readonly IRol _rolRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<RolDto> _validator;

    public RolService(IRol rolRepository, IMapper mapper, IValidator<RolDto> validator)
        : base(rolRepository, mapper, validator)
    {
        _rolRepository = rolRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<RolVm> GetRolNameByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("El nombre del rol no puede ser nulo o vacío.", nameof(name));
        }

        var rol = await _rolRepository.GetRolByName(name);
        
        if (rol == null) 
        {
            throw new KeyNotFoundException($"Rol no encontrado para el nombre: {name}");
        }
        return _mapper.Map<RolVm>(rol);

    }
}
