namespace MovieTracker.Models;

public class Actor
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte? Score { get; set; }
    
    //Relationships
    public virtual ICollection<Role>? Roles { get; set; }
}