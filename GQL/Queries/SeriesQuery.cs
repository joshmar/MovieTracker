using GraphQL.Types;
using MovieTracker.GQL.Types;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Queries;

public sealed class SeriesQuery : ObjectGraphType
{
    public SeriesQuery(ISeriesRepository seriesRepository)
    {
        FieldAsync<ListGraphType<SeriesType>>("series",
            resolve: async fieldContext => await seriesRepository.GetAllAsync(fieldContext.CancellationToken));
    }
}