using GraphQL.DataLoader;
using GraphQL.Types;
using MovieTracker.Models.Entities;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Types;

public sealed class MovieType : ObjectGraphType<Movie>
{
    public MovieType(IRoleService roleService, IDataLoaderContextAccessor dataLoaderAccessor)
    {
        Name = "Movie";
        Description = "Movie's basic information";
        Field(movie => movie.Id, nullable: false).Description("Movie Id.");
        Field(movie => movie.Title, nullable: false).Description("Title of the movie.");
        Field(movie => movie.Watched, nullable: false).Description("Field to track if you've watched the movie or not. By default, false.");
        
        Field(movie => movie.Description, nullable: true).Description("An optional description to be given to the movie.");
        Field(movie => movie.Score, nullable: true).Description("An optional score to be given to the movie.");
        
        FieldAsync<ListGraphType<RoleType>, IDataLoaderResult<IEnumerable<Role>>>(
            name: "roles",
            description: "Roles appearing in this movie.",
            resolve: context =>
            {
                var loader = dataLoaderAccessor.Context?
                    .GetOrAddCollectionBatchLoader<Guid, Role>("GetRolesByMovieId",
                        async movieIds =>
                        {
                            var toReturnList = new List<Role>();
                            foreach (var movieId in movieIds)
                                toReturnList.AddRange(await roleService.GetByEpisodeIdAsync(movieId));

                            return toReturnList.ToLookup(x => x.Id);
                        });
                var result = loader?.LoadAsync(context.Source.Id);
                return Task.FromResult(result);
            });
    }
}