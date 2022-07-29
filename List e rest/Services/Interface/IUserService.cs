using List_e_rest.ApiModel;

namespace List_e_rest.Services.Interface;

public interface IUserService : IBaseService<UserApiModel>
{
    Task<UserApiModel> GetUserByUserEmail(string email);
}