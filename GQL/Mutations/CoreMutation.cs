using GraphQL.Types;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Mutations;

public sealed class CoreMutation : ObjectGraphType
{
    public CoreMutation(IActorRepository actorRepository, IEpisodeRepository episodeRepository,
        IMovieRepository movieRepository, IRoleRepository roleRepository, ISeriesRepository seriesRepository)
    {
        foreach (var field in new ActorMutation(actorRepository).Fields)
        {
            AddField(field);
        }
    }
}