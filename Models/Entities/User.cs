namespace MovieTracker.Models.Entities
{
    public record User
    {
        public Guid Id { get; set; }
        public string? DisplayName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
