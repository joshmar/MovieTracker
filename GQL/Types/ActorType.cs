using GraphQL.Types;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Types;

public sealed class ActorType : ObjectGraphType<Actor>
{
    public ActorType(IRoleService roleService)
    {
        Name = "Actor";
        Description = "Actor's basic information";
        Field(actor => actor.Id, nullable: false).Description("Actor Id.");
        Field(actor => actor.FirstName, nullable: false).Description("Firstname of the actor.");
        Field(actor => actor.LastName, nullable: false).Description("Lastname of the actor.");
        Field(actor => actor.Score, nullable: true).Description("Personal score for the actor.");
        /*FieldAsync<ListGraphType<RoleType>>("Roles",
                resolve: async context => await roleService.GetByIdAsync(context.Source.Roles)
                )
            .Description("Roles of the actor");*/
    }
}