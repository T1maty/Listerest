using List_e_rest.ApiModel;

namespace List_e_rest.Services.Interface
{
    public interface IBaseService<T>
    {
        Task<CreateUpdate<T>> CreateAsync(T model);
        Task<List<CreateUpdate<T>>> CreateRangeAsync(List<T> models);
        Task<CreateUpdate<T>> UpdateAsync(T model);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<UserApiModel> GetUserByUserEmail(string email);
    }
}
