using GraphQL.Types;
using MovieTracker.GQL.Queries;

namespace MovieTracker.GQL.Schemas;

public class EpisodeSchema : Schema
{
    public EpisodeSchema(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<EpisodeQuery>();
    }
}