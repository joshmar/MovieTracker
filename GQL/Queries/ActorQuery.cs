using GraphQL.Types;
using MovieTracker.GQL.Types;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Queries;

public sealed class ActorQuery : ObjectGraphType
{
    public ActorQuery(IActorService actorService)
    {
        FieldAsync<ListGraphType<ActorType>>("actors", 
            resolve: async fieldContext =>  await actorService.GetAllAsync(fieldContext.CancellationToken));
    }
}