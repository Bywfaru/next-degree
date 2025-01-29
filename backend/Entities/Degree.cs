namespace DegreePlanner.Entities
{
    public class Degree
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
