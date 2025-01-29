namespace DegreePlanner.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
