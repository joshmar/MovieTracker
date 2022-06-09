using MovieTracker.Models;
using MovieTracker.Models.Entities;

namespace MovieTracker.Services.Interfaces;

public interface IMovieService : ICrudMethods<Movie, MovieModel>
{
    
}