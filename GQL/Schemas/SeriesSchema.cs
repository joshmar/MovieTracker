using GraphQL.Types;
using MovieTracker.GQL.Queries;

namespace MovieTracker.GQL.Schemas;

public class SeriesSchema : Schema
{
    public SeriesSchema(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<SeriesQuery>();
    }
}