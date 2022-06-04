namespace MovieTracker.Models;

public class RoleSerie
{
    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public Guid SerieId { get; set; }
    public Serie Serie { get; set; }
}