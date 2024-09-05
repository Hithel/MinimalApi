using AutoMapper;
using FluentValidation;
using MinimalApi.Repository.Interfaces;
using MinimalApi.Services.IServices;
using System.Linq.Expressions;

namespace MinimalApi.Services.Services;

public abstract class GenericService<Vm, Dto, T> : IGenericService<Vm, Dto> where Vm : class where Dto : class where T : class
{
    private readonly IGenericRepository<T> _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<Dto> _validator;


    protected GenericService(IGenericRepository<T> repository, IMapper mapper, IValidator<Dto> validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public virtual async Task<Vm?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        

    }

    public virtual async Task<IEnumerable<Vm>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();

    }

    public virtual async Task<IEnumerable<Vm>> FindByCondition(Expression<Func<Dto, bool>> predicate)
    {
        var entities = await _repository.FindByConditionAsync(e => predicate.Invoke(ConvertToDto(e)));

    }

    public virtual async Task<Vm> AddAsync(Dto dto)
    {
        await _repository.AddAsync(dto);

    }

    public virtual async Task<Vm> UpdateAsync(Dto dto, int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException("Entity not found");
        }

        await _repository.UpdateAsync(entity);
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException("Entity not found");
        }

        await _repository.RemoveAsync(entity);
    }
}