using GraphQL.Types;
using MovieTracker.Models.Entities;

namespace MovieTracker.GQL.Types;

public sealed class RoleType : ObjectGraphType<Role>
{
    public RoleType()
    {
        Name = "Role";
        Description = "Role that an actor plays";
        Field(role => role.Id, nullable: false).Description("Role Id.");
        Field(role => role.Name, nullable: false).Description("Name of the character.");
        Field(role => role.Description, nullable: true).Description("Optional description given for the role.");
        Field(role => role.Score, nullable: true).Description("Optional score given to the role.");
        
        //Relationships
        
        //Required:
        Field(role => role.ActorId, nullable: true).Description("Actors Id.");
        Field<ActorType>("Actor", "The linked actor.");
        
        //Optional:
        Field(role => role.EpisodeId, nullable: true).Description("Linked episode Id.");
        Field<EpisodeType>("Episode", "The linked episode.");
        Field(role => role.MovieId, nullable: true).Description("Linked movie Id.");
        Field<MovieType>("Movie", "The linked movie.");
        Field(role => role.SeriesId, nullable: true).Description("Linked series Id.");
        Field<SeriesType>("Series", "The linked series.");
    }
}