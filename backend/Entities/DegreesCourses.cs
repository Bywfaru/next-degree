namespace DegreePlanner.Entities
{
    public class DegreesCourses
    {
        public int Id { get; set; }
        public required int Degree { get; set; }
        public required string Course { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
