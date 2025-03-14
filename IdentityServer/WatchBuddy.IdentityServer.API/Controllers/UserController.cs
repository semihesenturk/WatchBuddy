using Duende.IdentityServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using WatchBuddy.IdentityServer.API.Dtos;
using WatchBuddy.IdentityServer.API.Models;
using WatchBuddy.Shared.ControllerBases;
using WatchBuddy.Shared.Dtos;

namespace WatchBuddy.IdentityServer.API.Controllers;

[Authorize(IdentityServerConstants.LocalApi.PolicyName)]
[Route("api/[controller]/[action]")]
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

    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
        if(userIdClaim == null) return BadRequest();
        
        var user = await userManager.FindByIdAsync(userIdClaim.Value);
        if(user == null) return BadRequest();
        
        var userDto = new ApplicationUser{Id = user.Id, UserName = user.UserName, Email = user.Email, FullName = user.FullName};
        return Ok(userDto);
    }
}