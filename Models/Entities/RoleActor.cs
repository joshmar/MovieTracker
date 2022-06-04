namespace MovieTracker.Models;

public class RoleActor
{
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    
    public Guid ActorId { get; set; }
    public Actor Actor { get; set; }
}