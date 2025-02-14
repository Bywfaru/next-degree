using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DegreePlanner.Entities;

public class Prerequisite : BaseEntity
{
    /**
     * The course that the prerequisite is associated with.
     */
    public string CourseId { get; set; }

    public Course? Course { get; set; }

    /**
     * The course that is required to be taken before the course.
     */
    public string PrerequisiteCourseId { get; set; }

    public Course? PrerequisiteCourse { get; set; }

    /**
     * Whether the prerequisite is a corequisite.
     */
    public bool IsCorequisite { get; set; }

    /**
     * The minimum grade required in the prerequisite course.
     */
    [Range(0, 1)]
    public double MinGrade { get; set; } = 0.5;

    /**
     * The user associated with the prerequisite.
     */
    [MaxLength(36)]
    public required string UserId { get; set; }

    [JsonIgnore] public ApplicationUser? User { get; set; }
}

public class CreatePrerequisiteDto
{
    /**
     * The course that the prerequisite is associated with.
     */
    public required string CourseId { get; set; }

    /**
     * The course that is required to be taken before the course.
     */
    public required string PrerequisiteCourseId { get; set; }

    /**
     * Whether the prerequisite is a corequisite.
     */
    public bool IsCorequisite { get; set; }

    /**
     * The minimum grade required in the prerequisite course.
     */
    public double MinGrade { get; set; } = 0.5;
}

public class UpdatePrerequisiteDto
{
    /**
     * The course that the prerequisite is associated with.
     */
    public string? CourseId { get; set; }

    /**
     * The course that is required to be taken before the course.
     */
    public string? PrerequisiteCourseId { get; set; }

    /**
     * Whether the prerequisite is a corequisite.
     */
    public bool? IsCorequisite { get; set; }

    /**
     * The minimum grade required in the prerequisite course.
     */
    public double? MinGrade { get; set; }
}