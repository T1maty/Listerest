using Microsoft.AspNetCore.Identity;
using List_e_rest.Helpers.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace List_e_rest.Models;

public class ListERestDbContext : IdentityDbContext<User, AppRole, int>
{
    public ListERestDbContext(DbContextOptions<ListERestDbContext> options)
        : base(options)
    {

    }

    
    //тут дописать модели: Table, School, Employee;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder
            .UseSqlServer("Server=.\\SQLExpress;Database=listerestSqlDb;Trusted_Connection=true;");
    }
    //тут дописать модели: Table, School, Employee;
    
    public override DbSet<User> Users { get; set; }
}