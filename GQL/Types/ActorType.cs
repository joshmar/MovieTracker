using GraphQL.Types;
using MovieTracker.Models.Entities;

namespace MovieTracker.GQL.Types;

public sealed class ActorType : ObjectGraphType<Actor>
{
    public ActorType()
    {
        Name = "Actor";
        Description = "Actor's basic information";
        Field(actor => actor.Id, nullable: false).Description("Actor Id.");
        Field(actor => actor.FirstName, nullable: false).Description("Firstname of the actor.");
        Field(actor => actor.LastName, nullable: false).Description("Lastname of the actor.");

        Field(actor => actor.Score, nullable: true).Description("Personal score for the actor.");

        FieldAsync<ListGraphType<RoleType>>(name: "roles", description: "Roles of the actor");
    }
}