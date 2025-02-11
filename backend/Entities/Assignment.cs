using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DegreePlanner.Entities;

public class Assignment : BaseEntity
{
    /**
     * The course that the assignment is associated with.
     */
    public int CourseId { get; set; }

    public Course? Course { get; set; }

    /**
     * The assignment category that the assignment is associated with.
     */
    public int AssignmentCategoryId { get; set; }

    public AssignmentCategory? AssignmentCategory { get; set; }

    /**
     * The name of the assignment.
     */
    [MaxLength(100)]
    public required string Name { get; set; }

    /**
     * The description of the assignment.
     */
    [MaxLength(1000)]
    public string Description { get; set; } = "";

    /**
     * The grade received on the assignment.
     */
    [Range(0, 999)]
    public double? GradeReceived { get; set; }

    /**
     * The highest expected possible grade on the assignment.
     */
    [Range(0, 999)]
    public required double GradePossible { get; set; }

    /**
     * The due date of the assignment.
     */
    public DateTime? DueDate { get; set; }

    /**
     * The date the assignment was completed.
     */
    public DateTime? CompletedDate { get; set; }

    /**
     * Whether the assignment is completed.
     */
    public bool IsCompleted { get; set; }

    /**
     * The user associated with the assignment.
     */
    [MaxLength(36)]
    public required string UserId { get; set; }

    [JsonIgnore] public ApplicationUser? User { get; set; }
}

public class CreateAssignmentDto
{
    public required int CourseId { get; set; }
    public required int AssignmentCategoryId { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = "";
    public double? GradeReceived { get; set; }
    public required double GradePossible { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public bool IsCompleted { get; set; }
}

public class UpdateAssignmentDto
{
    public int? CourseId { get; set; }
    public int? AssignmentCategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double? GradeReceived { get; set; }
    public double? GradePossible { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public bool? IsCompleted { get; set; }
}