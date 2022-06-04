namespace MovieTracker.Models;

public class RoleSeries
{
    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public Guid SeriesId { get; set; }
    public Series Series { get; set; }
}