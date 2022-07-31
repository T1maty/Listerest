using AutoMapper;
using List_e_rest.ApiModel;
using List_e_rest.Models;

namespace List_e_rest;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserApiModel, User>();
    }
}