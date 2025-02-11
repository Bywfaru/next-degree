using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DegreePlanner.Entities;

public class AssignmentCategory : BaseEntity
{
    /**
     * The name of the assignment category.
     */
    [MaxLength(100)]
    public required string Name { get; set; }

    /**
     * The course that the assignment category is associated with.
     */
    public required int CourseId { get; set; }

    [JsonIgnore] public Course? Course { get; set; }

    /**
     * The weight of the assignment category.
     */
    [Range(0, 999)]
    public required double Weight { get; set; }

    /**
     * The user associated with the assignment category.
     */
    [MaxLength(36)]
    public required string UserId { get; set; }

    [JsonIgnore] public ApplicationUser? User { get; set; }

    [JsonIgnore] public ICollection<Assignment> Assignments { get; } = new List<Assignment>();
}

public class CreateAssignmentCategoryDto
{
    /**
     * The name of the assignment category.
     */
    public required string Name { get; set; }

    /**
     * The course that the assignment category is associated with.
     */
    public required int CourseId { get; set; }

    /**
     * The weight of the assignment category.
     */
    public required double Weight { get; set; }
}

public class UpdateAssignmentCategoryDto
{
    /**
     * The name of the assignment category.
     */
    public string? Name { get; set; }

    /**
     * The course that the assignment category is associated with.
     */
    public int? CourseId { get; set; }

    /**
     * The weight of the assignment category.
     */
    public double? Weight { get; set; }
}