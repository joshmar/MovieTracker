using GraphQL;
using GraphQL.Types;
using MovieTracker.GQL.Types;
using MovieTracker.GQL.Types.InputTypes;
using MovieTracker.Models;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Mutations;

public class ActorMutation : ObjectGraphType
{
    public ActorMutation(IActorRepository actorRepository)
    {
        FieldAsync<ActorType>("createActor", 
            arguments: new QueryArguments(
            new QueryArgument<NonNullGraphType<ActorInputType>>{Name = "actor"}), 
            resolve: async context =>
            {
                var actor = context.GetArgument<ActorModel>("actor");
                return await actorRepository.CreateAsync(actor, context.CancellationToken);
            });
        
        FieldAsync<ActorType>("deleteActor", 
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<GuidGraphType>>{Name = "id"}), 
            resolve: async context =>
            {
                var actorId = context.GetArgument<Guid>("id");
                await actorRepository.DeleteAsync(actorId, context.CancellationToken);
                
                return await actorRepository.GetByIdAsync(actorId);
            });
        
        FieldAsync<ActorType>("updateActor", 
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<GuidGraphType>>{Name = "id"},
                new QueryArgument<NonNullGraphType<ActorInputType>>{Name = "actor"}), 
            resolve: async context =>
            {
                var actorId = context.GetArgument<Guid>("id");
                var actor = context.GetArgument<ActorModel>("actor");
                await actorRepository.UpdateAsync(actorId, actor, context.CancellationToken);
                return await actorRepository.GetByIdAsync(actorId);
            });
    }
}