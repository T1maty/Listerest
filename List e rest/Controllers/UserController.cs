using System.Net;
using List_e_rest.ApiModel;
using List_e_rest.ApiModel.RequestModels;
using List_e_rest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace List_e_rest.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public UserController(SignInManager<User> signInManager,UserManager<User> userManager,IConfiguration configuration)
    {
        this._signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]

    [ProducesResponseType(typeof(UserApiModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Register([FromBody] RegisterUser model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var newUser = await _userManager.FindByEmailAsync(user.Email);
                await _userManager.AddToRolesAsync(newUser, new[] { "User" });
            }
        }

        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(UserApiModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Login([FromBody] Login model)
    {
        var result =
            await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            await _signInManager.SignInAsync(user, false);
            
        }
        return NotFound();
    }
    
    
}