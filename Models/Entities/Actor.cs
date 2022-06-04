namespace MovieTracker.Models;

public class Actor
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte? Score { get; set; }
    public List<RoleActor> RoleActors { get; set; }
}