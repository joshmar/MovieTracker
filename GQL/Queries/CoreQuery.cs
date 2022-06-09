using GraphQL.Types;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Queries;

public sealed class CoreQuery : ObjectGraphType
{
    public CoreQuery(IActorRepository actorRepository, IEpisodeRepository episodeRepository, 
        IMovieRepository movieRepository, IRoleRepository roleRepository, ISeriesRepository seriesRepository)
    {
        foreach (var field in new ActorQuery(actorRepository).Fields)
        {
            this.AddField(field);
        }

        foreach (var field in new EpisodeQuery(episodeRepository).Fields)
        {
            this.AddField(field);
        }
        
        foreach (var field in new MovieQuery(movieRepository).Fields)
        {
            this.AddField(field);
        }
        
        foreach (var field in new RoleQuery(roleRepository).Fields)
        {
            this.AddField(field);
        }
        
        foreach (var field in new SeriesQuery(seriesRepository).Fields)
        {
            this.AddField(field);
        }
    }
}