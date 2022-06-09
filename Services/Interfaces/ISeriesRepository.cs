using MovieTracker.Models;
using MovieTracker.Models.Entities;

namespace MovieTracker.Services.Interfaces;

public interface ISeriesRepository : ICrudMethods<Series, SeriesModel>
{
    
}