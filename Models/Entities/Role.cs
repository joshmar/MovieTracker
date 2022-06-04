namespace MovieTracker.Models;

public class Role
{
    public Guid Id { get; set; }
    public Guid ActorId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public byte? Score { get; set; }
}