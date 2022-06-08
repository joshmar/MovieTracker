using GraphQL;
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

        FieldAsync<ActorType>("actor",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<GuidGraphType>>
            {
                Name = "id"
            }),
            resolve: async fieldContext =>
            {
                var id = fieldContext.GetArgument<Guid>("id");
                return await actorService.GetByIdAsync(id, fieldContext.CancellationToken);
            });
        
        FieldAsync<ListGraphType<ActorType>>("actorsByIds",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ListGraphType<GuidGraphType>>>
            {
                Name = "ids"
            }),
            resolve: async fieldContext =>
            {
                var id = fieldContext.GetArgument<IEnumerable<Guid>>("ids");
                return await actorService.GetByIdsAsync(id, fieldContext.CancellationToken);
            });
    }
}