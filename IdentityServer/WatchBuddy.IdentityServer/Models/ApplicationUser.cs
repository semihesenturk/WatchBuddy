using Microsoft.AspNetCore.Identity;

namespace WatchBuddy.IdentityServer.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
}