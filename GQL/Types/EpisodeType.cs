using GraphQL.DataLoader;
using GraphQL.Types;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Types;

public sealed class EpisodeType : ObjectGraphType<Episode>
{
    public EpisodeType(IRoleService roleService, IDataLoaderContextAccessor dataLoaderAccessor)
    {
        Name = "Episode";
        Description = "Episode's basic information";
        Field(episode => episode.Id, nullable: false).Description("Episode Id.");
        Field(episode => episode.Title, nullable: false).Description("The title of the episode.");
        Field(episode => episode.Watched, nullable: false)
            .Description("Field to track if you've watched the episode or not. By default, false.");

        Field(episode => episode.SeriesId, nullable: false).Description("The id of the linked series.");
        Field<SeriesType>("Series", "The series object that this episode is from.");

        Field(episode => episode.Description, nullable: true)
            .Description("An optional description to be given to the episode.");
        Field(episode => episode.Score, nullable: true)
            .Description("An optional score to be given to the episode.");

        FieldAsync<ListGraphType<RoleType>, IDataLoaderResult<IEnumerable<Role>>>(
            name: "roles",
            description: "Roles appearing in this episode.",
            resolve: context =>
            {
                var loader = dataLoaderAccessor.Context?
                    .GetOrAddCollectionBatchLoader<Guid, Role>("GetRolesByEpisodeId",
                        async episodeIds =>
                        {
                            var toReturnList = new List<Role>();
                            foreach (var episodeId in episodeIds)
                                toReturnList.AddRange(await roleService.GetByEpisodeIdAsync(episodeId));

                            return toReturnList.ToLookup(x => x.Id);
                        });
                var result = loader?.LoadAsync(context.Source.Id);
                return Task.FromResult(result);
            });
    }
}