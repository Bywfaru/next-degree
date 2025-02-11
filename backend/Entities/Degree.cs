using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DegreePlanner.Services;

namespace DegreePlanner.Entities;

public class Degree : BaseEntity
{
    /**
     * The user associated with the degree.
     */
    [MaxLength(36)]
    public required string UserId { get; set; }

    [JsonIgnore] public ApplicationUser? User { get; set; }

    /**
     * The name of the degree.
     */
    [MaxLength(100)]
    public required string Name { get; set; }

    /**
     * The progress status of the degree.
     */
    public Status Status { get; set; } = Status.NotStarted;

    [JsonIgnore] public ICollection<DegreeCourse> DegreeCourses { get; } = new List<DegreeCourse>();
}

public class CreateDegreeRequestDto
{
    /**
     * The name of the degree.
     */
    public required string Name { get; set; }

    /**
     * The progress status of the degree.
     */
    public Status Status { get; set; } = Status.NotStarted;
}

public class UpdateDegreeRequestDto
{
    /**
     * The name of the degree.
     */
    public string? Name { get; set; }

    /**
     * The progress status of the degree.
     */
    public Status? Status { get; set; }
}

public class DegreeResponseDto : Degree
{
    /**
     * The number of credits completed in the degree.
     */
    public double CompletedCredits { get; set; }

    /**
     * The total number of credits in the degree.
     */
    public double TotalCredits { get; set; }
}