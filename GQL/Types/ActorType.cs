using GraphQL.DataLoader;
using GraphQL.Types;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Types;

public sealed class ActorType : ObjectGraphType<Actor>
{
    public ActorType(IRoleService roleService, IDataLoaderContextAccessor dataLoaderAccessor)
    {
        Name = "Actor";
        Description = "Actor's basic information";
        Field(actor => actor.Id, nullable: false).Description("Actor Id.");
        Field(actor => actor.FirstName, nullable: false).Description("Firstname of the actor.");
        Field(actor => actor.LastName, nullable: false).Description("Lastname of the actor.");

        Field(actor => actor.Score, nullable: true).Description("Personal score for the actor.");

        FieldAsync<ListGraphType<RoleType>, IDataLoaderResult<IEnumerable<Role>>>(
            name: "roles",
            description: "Roles of the actor",
            resolve: context =>
            {
                var loader = dataLoaderAccessor.Context?
                    .GetOrAddCollectionBatchLoader<Guid, Role>("GetRolesByActorId",
                        async actorIds =>
                        {
                            var toReturnList = new List<Role>();
                            foreach (var actorId in actorIds)
                                toReturnList.AddRange(await roleService.GetByActorIdAsync(actorId));

                            return toReturnList.ToLookup(x => x.Id);
                        });
                var result = loader?.LoadAsync(context.Source.Id);
                return Task.FromResult(result);
            });
    }
}