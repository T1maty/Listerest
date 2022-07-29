using List_e_rest.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
namespace List_e_rest.Models
{
    public class User :   IdentityUser<int>,  IBaseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
