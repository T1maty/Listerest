using Microsoft.AspNetCore.Identity;

namespace List_e_rest.Helpers.Seed;

public class AppRole : IdentityRole<int>
{
    public AppRole()
    {
            
    }

    public AppRole(string rolename)
    {
        Name = rolename;
        NormalizedName = rolename.ToUpper();
    }

    public string Name { get; }
    public string NormalizedName { get; }
}