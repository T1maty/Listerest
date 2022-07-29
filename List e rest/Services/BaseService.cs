using AutoMapper;
using List_e_rest.ApiModel;
using List_e_rest.Models;
using List_e_rest.Models.Interfaces;
using List_e_rest.Repository;
using List_e_rest.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace List_e_rest.Services
{
    public class BaseService<TApiModel, TModel> : IBaseService<TApiModel> where TModel : class, IBaseModel where TApiModel : class
    {
        protected readonly IMapper _mapper;
        protected readonly IBaseRepository<TModel> _repository;
        
        public async Task<CreateUpdate<TApiModel>> CreateAsync(TApiModel model)
        {
            var res = await _repository.CreateAsync(_mapper.Map<TModel>(model));
            var mapped = _mapper.Map<TApiModel>(res);
            return new CreateUpdate<TApiModel> { Success = true, Model = mapped };
        }

        public async Task<List<CreateUpdate<TApiModel>>> CreateRangeAsync(List<TApiModel> models)
        {
            var res = await _repository.CreateRangeAsync(_mapper.Map<List<TModel>>(models));
            var mapped = _mapper.Map<List<TApiModel>>(res);
            var list = new List<CreateUpdate<TApiModel>>();
            mapped.ForEach(m => list.Add(new CreateUpdate<TApiModel> { Success = true, Model = m }));
            return list;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteByIdAsync(id);
        }

        public async Task<List<TApiModel>> GetAllAsync()
        {
            var res = await _repository.GetAllAsync();
            return _mapper.Map<List<TApiModel>>(res);
        }

        public async Task<TApiModel> GetByIdAsync(int id)
        {
            var res = await _repository.GetByAsync(x => x.Id == id);
            return res == null ? null : _mapper.Map<TApiModel>(res);
        }
        
        
        public Task<UserApiModel> GetUserByUserEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<CreateUpdate<TApiModel>> UpdateAsync(TApiModel model)
        {
            var res = await _repository.UpdateAsync(_mapper.Map<TModel>(model));
            return res == null
                ? null
                : new CreateUpdate<TApiModel> { Success = true, Model = _mapper.Map<TApiModel>(res) };
        }

    }
}
