﻿using AutoMapper;
using FluentValidation;
using MinimalApi.Repository.Interfaces;

using MinimalApi.Services.IServices;
using System.Linq.Expressions;

namespace MinimalApi.Services.Implementations
{
    public abstract class GenericService<Vm, Dto, T> : IGenericService<Vm, Dto>
        where Vm : class
        where Dto : class
        where T : class
    {
        protected readonly IGenericRepository<T> _repository;
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
            return _mapper.Map<Vm>(entity);
        }

        public virtual async Task<IEnumerable<Vm>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
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
            await _repository.AddAsync(entity);

            return _mapper.Map<Vm>(entity);
        }

        public virtual async Task<Vm> UpdateAsync(Dto dto, int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            }
            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);

            return _mapper.Map<Vm>(entity);
        }
    }
    
}
