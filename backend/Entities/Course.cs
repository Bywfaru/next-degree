using System.ComponentModel.DataAnnotations;

namespace DegreePlanner.Entities
{
    public class Course
    {
        [Key]
        public required string CourseNumber { get; set; }
        public required string CourseName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public double PassingGrade { get; set; } = 0.5;
        public required double Credits { get; set; }
    }
}
