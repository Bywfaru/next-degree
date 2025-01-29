namespace DegreePlanner.Entities
{
    public class Prerequisite
    {
        public int Id { get; set; }
        public required string Course { get; set; }
        public required string Prereq { get; set;  }
        public double MinGrade { get; set; } = 0.5;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
