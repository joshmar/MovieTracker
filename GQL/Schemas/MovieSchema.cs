using GraphQL.Types;
using MovieTracker.GQL.Queries;

namespace MovieTracker.GQL.Schemas;

public class MovieSchema : Schema
{
    public MovieSchema(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<MovieQuery>();
    }
}