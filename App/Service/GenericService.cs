using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.ViewModels;

using AutoMapper;
using App.Intefaces.Repositories;
using Domain.Entities;
using App.Intefaces.Services;

namespace App.Service
{
    public class GenericService<TEntity, TViewModel> : IGenericService<TEntity, TViewModel>
        where TEntity : class
        where TViewModel : class
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TViewModel>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TViewModel>>(entities); // Mapea entidades a modelos de vista
        }

        public async Task<TViewModel> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TViewModel>(entity); // Mapea entidad a modelo de vista
        }

        public async Task AddAsync(TViewModel viewModel)
        {
            var entity = _mapper.Map<TEntity>(viewModel); // Mapea modelo de vista a entidad
            await _repository.AddAsync(entity);
        }

        public void Update(TViewModel viewModel)
        {
            var entity = _mapper.Map<TEntity>(viewModel); // Mapea modelo de vista a entidad
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _repository.GetByIdAsync(id).Result;
            _repository.Delete(entity);
        }
    }
}
