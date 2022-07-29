 using List_e_rest.Models.Interfaces;
namespace List_e_rest.Repository
{
    public interface IBaseRepository<TModel> where TModel : class, IBaseModel
    {
        Task<TModel> CreateAsync(TModel model);
        Task<IEnumerable<TModel>> CreateRangeAsync(ICollection<TModel> models);
        Task<TModel> UpdateAsync(TModel model);
        Task<bool> DeleteByIdAsync(int id);
        Task<TModel> GetByAsync(Func<TModel, bool> filter);
        Task<List<TModel>> GetAllAsync();
        Task<List<TModel>> GetAllAsync(Func<TModel, bool> filter);
        
    }
}
