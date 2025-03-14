using System.ComponentModel.DataAnnotations;

namespace WatchBuddy.IdentityServer.API.Dtos;

public class SignupDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string FullName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}