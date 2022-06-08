using GraphQL.Types;
using MovieTracker.GQL.Types;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Queries;

public sealed class SeriesQuery : ObjectGraphType
{
    public SeriesQuery(ISeriesService seriesService)
    {
        FieldAsync<ListGraphType<SeriesType>>("series",
            resolve: async fieldContext => await seriesService.GetAllAsync(fieldContext.CancellationToken));
    }
}