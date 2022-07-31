using List_e_rest.Helpers.Seed;
using List_e_rest.Models;
using List_e_rest.Services;
using List_e_rest.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ListERestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("listerestSqlDb")));
builder.Services.AddIdentity<User, AppRole>(options =>
    {
        options.User.RequireUniqueEmail = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<ListERestDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
