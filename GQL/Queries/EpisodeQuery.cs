using GraphQL.Types;
using MovieTracker.GQL.Types;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Queries;

public sealed class EpisodeQuery : ObjectGraphType
{
    public EpisodeQuery(IEpisodeService episodeService)
    {
        FieldAsync<ListGraphType<EpisodeType>>("Episodes",
            resolve: async fieldContext => await episodeService.GetAllAsync(fieldContext.CancellationToken));
    }
}