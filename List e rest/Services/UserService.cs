using AutoMapper;
using List_e_rest.ApiModel;
using List_e_rest.Helpers.Models;
using List_e_rest.Helpers.Seed;
using List_e_rest.Models;
using List_e_rest.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using List_e_rest.Repository.Interfaces;
using List_e_rest.Services.Interface;

namespace List_e_rest.Services
{
    public class UserService : BaseService<UserApiModel, User>, IUserService
    {
        private new readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, UserManager<User> userManager, IUserRepository userRepository, RoleManager<AppRole> roleManager) 
        {
            _mapper = mapper;
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
