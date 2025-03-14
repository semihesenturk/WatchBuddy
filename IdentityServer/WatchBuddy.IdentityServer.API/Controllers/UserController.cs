using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchBuddy.IdentityServer.API.Dtos;
using WatchBuddy.IdentityServer.API.Models;
using WatchBuddy.Shared.ControllerBases;
using WatchBuddy.Shared.Dtos;

namespace WatchBuddy.IdentityServer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(UserManager<ApplicationUser> userManager) : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> SignUp(SignupDto signupDto)
    {
        var user = new ApplicationUser
            { UserName = signupDto.UserName, Email = signupDto.Email, FullName = signupDto.FullName };
        var result = await userManager.CreateAsync(user, signupDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(BaseServiceResponse<NoContent>.Fail(
                result.Errors.Select(x => x.Description).ToList(), 400));
        }

        return NoContent();
    }
}