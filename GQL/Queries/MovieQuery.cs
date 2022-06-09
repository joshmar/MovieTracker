using GraphQL.Types;
using MovieTracker.GQL.Types;
using MovieTracker.Services.Interfaces;

namespace MovieTracker.GQL.Queries;

public sealed class MovieQuery : ObjectGraphType
{
    public MovieQuery(IMovieRepository movieRepository)
    {
        FieldAsync<ListGraphType<MovieType>>("Movies",
            resolve: async fieldContext => await movieRepository.GetAllAsync(fieldContext.CancellationToken));
    }
}