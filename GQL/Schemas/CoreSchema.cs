using GraphQL.Types;
using MovieTracker.GQL.Mutations;
using MovieTracker.GQL.Queries;

namespace MovieTracker.GQL.Schemas;

public class CoreSchema : Schema
{
    public CoreSchema(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<CoreQuery>();
        Mutation = serviceProvider.GetRequiredService<CoreMutation>();
    }
}