using GraphQL.Types;
using MovieTracker.GQL.Queries;

namespace MovieTracker.GQL.Schemas;

public class ActorSchema : Schema
{
    public ActorSchema(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<ActorQuery>();
    }
}