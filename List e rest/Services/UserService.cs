using List_e_rest.ApiModel;
using List_e_rest.Helpers.Models;
using List_e_rest.Models;
using List_e_rest.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using List_e_rest.Repository.Interfaces;
using List_e_rest.Services.Interface;

namespace List_e_rest.Services
{
    public class UserService : BaseService<UserApiModel, User>, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<User> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<UserApiModel> GetUserByUserEmail(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null) throw new ApiException("find user by this email");
            return _mapper.Map<UserApiModel>(user);

        }
    }
}
