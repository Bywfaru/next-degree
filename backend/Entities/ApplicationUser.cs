using DegreePlanner.Services;
using Microsoft.AspNetCore.Identity;

namespace DegreePlanner.Entities;

public class ApplicationUser : IdentityUser, IAuditable
{
    /**
     * The date and time that the user was created.
     */
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /**
     * The date and time that the user was last updated.
     */
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}