using Duende.IdentityModel;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Identity;
using WatchBuddy.IdentityServer.API.Models;

namespace WatchBuddy.IdentityServer.API.Services;

public class IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
    : IResourceOwnerPasswordValidator
{
    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var existingUser = await userManager.FindByEmailAsync(context.UserName);
        if (existingUser == null)
        {
            var errors = new Dictionary<string, object> { { "errors", new List<string> { "Invalid username or password." } } };
            context.Result.CustomResponse =errors;
            return;
        }
        
        var passwordCheck = await userManager.CheckPasswordAsync(existingUser, context.Password);
        if (!passwordCheck)
        {
            var errors = new Dictionary<string, object> { { "errors", new List<string> { "Invalid password." } } };
            context.Result.CustomResponse = errors;
            return;
        }
        
        context.Result = new GrantValidationResult(existingUser.Id, OidcConstants.AuthenticationMethods.Password);
    }
}