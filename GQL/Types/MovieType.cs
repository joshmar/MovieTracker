using GraphQL.Types;
using MovieTracker.Models.Entities;

namespace MovieTracker.GQL.Types;

public sealed class MovieType : ObjectGraphType<Movie>
{
    public MovieType()
    {
        Name = "Actor";
        Description = "Actor's basic information";
        Field(movie => movie.Id, nullable: false).Description("Movie Id.");
        Field(movie => movie.Title, nullable: false).Description("Title of the movie.");
        Field(movie => movie.Watched, nullable: false).Description("Field to track if you've watched the movie or not. By default, false.");
        
        Field(movie => movie.Description, nullable: true).Description("An optional description to be given to the movie.");
        Field(movie => movie.Score, nullable: true).Description("An optional score to be given to the movie.");
        /*Field<ListGraphType<RoleType>>("Roles",
                arguments: new QueryArguments(new QueryArgument<graphtype>()))
            .Description("Roles of the actor");*/
    }
}