using GraphQL.DataLoader;
using GraphQL.Types;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Types;

public sealed class SeriesType : ObjectGraphType<Series>
{
    public SeriesType(IRoleService roleService, IEpisodeService episodeService, IDataLoaderContextAccessor dataLoaderAccessor)
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
            description: "Roles appearing in this series.",
            resolve: context =>
            {
                var loader = dataLoaderAccessor.Context?
                    .GetOrAddCollectionBatchLoader<Guid, Role>("GetRolesBySeriesId",
                        async seriesIds =>
                        {
                            var toReturnList = new List<Role>();
                            foreach (var seriesId in seriesIds)
                                toReturnList.AddRange(await roleService.GetBySeriesIdAsync(seriesId));

                            return toReturnList.ToLookup(x => x.Id);
                        });
                var result = loader?.LoadAsync(context.Source.Id);
                return Task.FromResult(result);
            });
        
        FieldAsync<ListGraphType<EpisodeType>, IDataLoaderResult<IEnumerable<Episode>>>(
            name: "episodes",
            description: "Episodes for this series.",
            resolve: context =>
            {
                var loader = dataLoaderAccessor.Context?
                    .GetOrAddCollectionBatchLoader<Guid, Episode>("GetEpisodesBySeriesId",
                        async episodeIds =>
                        {
                            var toReturnList = new List<Episode>();
                            foreach (var episodeId in episodeIds)
                                toReturnList.AddRange(await episodeService.GetBySeriesIdAsync(episodeId));

                            return toReturnList.ToLookup(x => x.Id);
                        });
                var result = loader?.LoadAsync(context.Source.Id);
                return Task.FromResult(result);
            });
    }
}