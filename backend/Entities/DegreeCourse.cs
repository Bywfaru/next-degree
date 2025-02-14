using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DegreePlanner.Entities;

public class DegreeCourse : BaseEntity
{
    /**
     * The degree associated with the course.
     */
    public required string DegreeId { get; set; }

    [JsonIgnore] public Degree? Degree { get; set; }

    /**
     * The course associated with the degree.
     */
    public required string CourseId { get; set; }

    [JsonIgnore] public Course? Course { get; set; }

    /**
     * the user associated with the degree-course association.
     */
    [MaxLength(36)]
    public required string UserId { get; set; }

    [JsonIgnore] public ApplicationUser? User { get; set; }
}

public class CreateDegreeCourseDto
{
    /**
     * The degree associated with the course.
     */
    public required string DegreeId { get; set; }

    /**
     * The course associated with the degree.
     */
    public required string CourseId { get; set; }
}