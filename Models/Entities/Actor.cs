using System.ComponentModel.DataAnnotations;

namespace MovieTracker.Models;

public class Actor
{
    public Guid Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public byte? Score { get; set; }
    public List<RoleActor> RoleActors { get; set; }
}