using Microsoft.AspNetCore.Http;

namespace WatchBuddy.Shared.Services;

public class SharedIdentityService(IHttpContextAccessor httpContextAccessor) : ISharedIdentityService
{
    public string GetUserId => httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
}