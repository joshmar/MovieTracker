using GraphQL.Types;
using MovieTracker.Models.Entities;

namespace MovieTracker.GQL.Types;

public class SeriesType : ObjectGraphType<Series>
{
    public SeriesType()
    {
        Name = "Series";
        Description = "Series basic information";
        Field(episode => episode.Id, nullable: false).Description("Series Id.");
        Field(episode => episode.Title, nullable: false).Description("The title of the series.");
        Field(episode => episode.Watched, nullable: false).Description("Field to track if you've watched the complete series or not. By default, false.");

        Field(episode => episode.Description, nullable: true).Description("An optional description to be given to the series.");
        Field(episode => episode.Score, nullable: true).Description("An optional score to be given to the series.");
        /*Field<ListGraphType<RoleType>>("Roles",
                arguments: new QueryArguments(new QueryArgument<graphtype>()))
            .Description("Roles of the actor");*/
    }
}