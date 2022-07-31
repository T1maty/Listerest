using AutoMapper;

namespace List_e_rest;

public  static class AutoMapperConfiguration
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var config = new MapperConfiguration(m =>
        {
            m.AddProfile(new MappingProfiles());
        });

        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}