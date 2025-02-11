using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DegreePlanner.Services;

namespace DegreePlanner.Entities;

public class Course : BaseEntity
{
    /**
     * The name of the course.
     */
    [MaxLength(100)]
    public required string Name { get; set; }

    /**
     * The course code.
     */
    [MaxLength(10)]
    public required string Code { get; set; }

    /**
     * The grade that the user needs to pass the course.
     */
    [Range(0, 1)]
    public double PassingGrade { get; set; } = 0.5;

    /**
     * The credits that the course is worth.
     */
    [Range(0, 10)]
    public required double Credits { get; set; }

    /**
     * The user associated with the course.
     */
    [MaxLength(36)]
    public required string UserId { get; set; }

    [JsonIgnore] public ApplicationUser? User { get; set; }

    /**
     * The progress status of the course.
     */
    public Status Status { get; set; } = Status.NotStarted;

    [JsonIgnore] public ICollection<Assignment> Assignments { get; } = new List<Assignment>();
    [JsonIgnore] public ICollection<AssignmentCategory> AssignmentCategories { get; } = new List<AssignmentCategory>();
    [JsonIgnore] public ICollection<Prerequisite> CoursesRequiringThis { get; } = new List<Prerequisite>();
    [JsonIgnore] public ICollection<Prerequisite> Prerequisites { get; } = new List<Prerequisite>();
    [JsonIgnore] public ICollection<DegreeCourse> DegreeCourses { get; } = new List<DegreeCourse>();
}

public class CreateCourseDto
{
    /**
     * The name of the course.
     */
    public required string Name { get; set; }

    /**
     * The course code.
     */
    public required string Code { get; set; }

    /**
     * The credits that the course is worth.
     */
    public required double Credits { get; set; }

    /**
     * The grade that the user needs to pass the course.
     */
    public double PassingGrade { get; set; } = 0.5;

    /**
     * The progress status of the course.
     */
    public Status Status { get; set; } = Status.NotStarted;
}

public class UpdateCourseDto
{
    /**
     * The name of the course.
     */
    public string? Name { get; set; }

    /**
     * The course code.
     */
    public string? Code { get; set; }

    /**
     * The credits that the course is worth.
     */
    public double? Credits { get; set; }

    /**
     * The grade that the user needs to pass the course.
     */
    public double? PassingGrade { get; set; }

    /**
     * The progress status of the course.
     */
    // [JsonIgnore]
    public Status? Status { get; set; }
}