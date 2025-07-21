using Microsoft.AspNetCore.Identity;

namespace Domain.User;

public class User : IdentityUser
{
    public string? Initials { get; set; }
}
