using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using testing_net.Models;
using testing_net.Repositories.Database;
using testing_net.Repositories.Interfaces;

namespace testing_net.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository 
    {
        public MovieRepository(DataBaseContext context) : base(context)
        {
        }

        IEnumerable<Movie> IMovieRepository.GetLastMovies(int amount)
        {
            return Context.Movies.OrderBy(m => m.ReleaseDate).Take(amount);
        }
    }
}
