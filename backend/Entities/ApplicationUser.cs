using DegreePlanner.Services;
using Microsoft.AspNetCore.Identity;

namespace DegreePlanner.Entities;

public class ApplicationUser : IdentityUser, IAuditable
{
    // /**
    //  * The first name of the user.
    //  */
    // public required string FirstName { get; set; } = null!;
    //
    // /**
    //  * The last name of the user.
    //  */
    // public required string LastName { get; set; } = null!;

    /**
     * The date and time that the user was created.
     */
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /**
     * The date and time that the user was last updated.
     */
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
