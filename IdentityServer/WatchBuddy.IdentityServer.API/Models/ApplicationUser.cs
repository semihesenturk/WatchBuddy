using Microsoft.AspNetCore.Identity;

namespace WatchBuddy.IdentityServer.API.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
}