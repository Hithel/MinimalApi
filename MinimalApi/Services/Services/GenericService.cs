using AutoMapper;
using FluentValidation;
using MinimalApi.Repository.Interfaces;
using MinimalApi.Repository.UnitOfWork;
using MinimalApi.Services.IServices;
using System.Linq.Expressions;

namespace MinimalApi.Services.Services
{
    public abstract class GenericService<Vm, Dto, T> : IGenericService<Vm, Dto>
        where Vm : class
        where Dto : class
        where T : class
    {
        protected readonly IUnitOfWork _unitOfWork; // Unidad de Trabajo
        private readonly IMapper _mapper;
        private readonly IValidator<Dto> _validator;

        protected GenericService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<Dto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        // Método abstracto que será implementado por cada servicio concreto para obtener el repositorio correcto
        protected abstract IGenericRepository<T> GetRepository();

        public virtual async Task<Vm?> GetByIdAsync(int id)
        {
            var entity = await GetRepository().GetByIdAsync(id);
            return _mapper.Map<Vm>(entity);
        }

        public virtual async Task<IEnumerable<Vm>> GetAllAsync()
        {
            var entities = await GetRepository().GetAllAsync();
            return _mapper.Map<IEnumerable<Vm>>(entities);
        }


        public virtual async Task<Vm> AddAsync(Dto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var entity = _mapper.Map<T>(dto);
            await GetRepository().AddAsync(entity);
            await _unitOfWork.SaveAsync(); // Guardar cambios con UnitOfWork

            return _mapper.Map<Vm>(entity);
        }

        public virtual async Task<Vm> UpdateAsync(Dto dto, int id)
        {
            var entity = await GetRepository().GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            }

            _mapper.Map(dto, entity);
            await GetRepository().UpdateAsync(entity);
            await _unitOfWork.SaveAsync(); // Guardar cambios con UnitOfWork

            return _mapper.Map<Vm>(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetRepository().GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            }

            await GetRepository().RemoveAsync(entity);
            await _unitOfWork.SaveAsync(); // Guardar cambios con UnitOfWork
        }

        private Dto ConvertToDto(T entity)
        {
            return _mapper.Map<Dto>(entity);
        }
    }
}
