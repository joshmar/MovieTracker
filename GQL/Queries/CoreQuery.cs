using GraphQL.Types;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Queries;

public sealed class CoreQuery : ObjectGraphType
{
    public CoreQuery(IActorService actorService, IEpisodeService episodeService, 
        IMovieService movieService, IRoleService roleService, ISeriesService seriesService)
    {
        foreach (var field in new ActorQuery(actorService).Fields)
        {
            this.AddField(field);
        }

        foreach (var field in new EpisodeQuery(episodeService).Fields)
        {
            this.AddField(field);
        }
        
        foreach (var field in new MovieQuery(movieService).Fields)
        {
            this.AddField(field);
        }
        
        foreach (var field in new RoleQuery(roleService).Fields)
        {
            this.AddField(field);
        }
        
        foreach (var field in new SeriesQuery(seriesService).Fields)
        {
            this.AddField(field);
        }
    }
}