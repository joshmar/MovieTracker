using GraphQL.Types;
using MovieTracker.GQL.Types;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Queries;

public sealed class MovieQuery : ObjectGraphType
{
    public MovieQuery(IMovieService movieService)
    {
        FieldAsync<ListGraphType<MovieType>>("Movies",
            resolve: async fieldContext => await movieService.GetAllAsync(fieldContext.CancellationToken));
    }
}