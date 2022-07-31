using AutoMapper;
using List_e_rest.Models;
using List_e_rest.Repository.Interfaces;

namespace List_e_rest.Repository;

public class UserRepository: BaseRepository<User>, IUserRepository
{
    public UserRepository(ListERestDbContext listerestDbcontext, ILogger<BaseRepository<User>> logger, IMapper mapper) : base(listerestDbcontext, logger, mapper)
    {
    }
}

