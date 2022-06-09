using GraphQL.DataLoader;
using GraphQL.Types;
using MovieTracker.Models.Entities;

namespace MovieTracker.GQL.Types;

public sealed class SeriesType : ObjectGraphType<Series>
{
    public SeriesType()
    {
        Name = "Series";
        Description = "Series basic information";
        Field(series => series.Id, nullable: false).Description("Series Id.");
        Field(series => series.Title, nullable: false).Description("The title of the series.");
        Field(series => series.Watched, nullable: false)
            .Description("Field to track if you've watched the complete series or not. By default, false.");

        Field(series => series.Description, nullable: true).Description("An optional description to be given to the series.");
        Field(series => series.Score, nullable: true).Description("An optional score to be given to the series.");
        
        FieldAsync<ListGraphType<RoleType>, IDataLoaderResult<IEnumerable<Role>>>(
            name: "roles",
            description: "Roles appearing in this series.");
        
        FieldAsync<ListGraphType<EpisodeType>, IDataLoaderResult<IEnumerable<Episode>>>(
            name: "episodes",
            description: "Episodes for this series.");
    }
}