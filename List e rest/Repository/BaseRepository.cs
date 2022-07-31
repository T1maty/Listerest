using AutoMapper;
using List_e_rest.Models;
using List_e_rest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using List_e_rest.Models.Interfaces;

namespace List_e_rest.Repository
{
    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class, IBaseModel
    {
        protected readonly ILogger<BaseRepository<TModel>> _logger;
        protected readonly ListERestDbContext _listERestDb;
        protected readonly DbSet<TModel> _dbSet;
        protected readonly IMapper _mapper;

        protected BaseRepository(ListERestDbContext glitchContext, ILogger<BaseRepository<TModel>> logger,
            IMapper mapper)
        {
            _listERestDb = glitchContext;
            _logger = logger;
            _dbSet = glitchContext.Set<TModel>();
            _mapper = mapper;
        }

        public virtual async Task<TModel> CreateAsync(TModel model)
        {
            try
            {
                model.Id = 0;
                await _dbSet.AddAsync(model);
                await _listERestDb.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public virtual async Task<IEnumerable<TModel>> CreateRangeAsync(ICollection<TModel> models)
        {
            try
            {
                foreach (var model in models)
                {
                    model.Id = 0;
                }

                await _dbSet.AddRangeAsync(models);
                await _listERestDb.SaveChangesAsync();
                return models;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public virtual async Task<TModel> UpdateAsync(TModel model)
        {
            try
            {
                var local = _dbSet
                    .Local
                    .FirstOrDefault(entry => entry.Id.Equals(model.Id));

                // check if local is not null 
                if (local != null)
                {
                    // detach
                    _listERestDb.Entry(local).State = EntityState.Detached;
                }

                _dbSet.Attach(model);
                _listERestDb.Entry(model).State = EntityState.Modified;
                await _listERestDb.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            try
            {
                var found = await _dbSet.FindAsync(id);
                if (found == null)
                    return false;
                _dbSet.Remove(found);
                await _listERestDb.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public virtual async Task<bool> DeleteRangeAsync(IEnumerable<TModel> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
                await _listERestDb.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public virtual Task<TModel> GetByAsync(Func<TModel, bool> filter)
        {
            var res = _dbSet.AsNoTracking().SingleOrDefault(filter);
            return Task.FromResult(res);
        }

        public virtual Task<List<TModel>> GetAllAsync()
        {
            var res = _dbSet.AsNoTracking().ToList();
            return Task.FromResult(res);
        }

        public virtual Task<List<TModel>> GetAllAsync(Func<TModel, bool> filter)
        {
            var res = _dbSet.AsNoTracking().Where(filter).ToList();
            return Task.FromResult(res);
        }
    }
}