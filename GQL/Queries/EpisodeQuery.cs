using GraphQL.Types;
using MovieTracker.GQL.Types;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Queries;

public sealed class EpisodeQuery : ObjectGraphType
{
    public EpisodeQuery(IEpisodeRepository episodeRepository)
    {
        FieldAsync<ListGraphType<EpisodeType>>("Episodes",
            resolve: async fieldContext => await episodeRepository.GetAllAsync(fieldContext.CancellationToken));
    }
}