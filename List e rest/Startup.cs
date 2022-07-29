using List_e_rest.Helpers.Seed;
using AutoMapper;
using List_e_rest;
using List_e_rest.ApiModel;
using List_e_rest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using List_e_rest.Services;
using List_e_rest.Services.Interface;
using List_e_rest.Repository.Interfaces;
using List_e_rest.Repository;

namespace Los_Pollos_Hermanos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });
        }
        private MapperConfiguration MapperConfiguration { get; }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ListERestDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("GlitchSqlDb")));

            services.AddSingleton(c => MapperConfiguration.CreateMapper());

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader());
                // .AllowAnyOrigin());
            });
            services.AddMvc();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient(typeof(IBaseService<>));
            services.AddTransient<SignInManager<User>>();

            services.AddIdentity<User, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ListERestDbContext>()

                .AddDefaultTokenProviders();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" }); });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            
            
            app.UseSwagger();
            

            
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
          
        }
    }
}