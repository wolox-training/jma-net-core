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

        public IEnumerable<string> GetGenres()
        {
            return Context.Movies.OrderBy(m => m.Genre).Select(m => m.Genre).Distinct();
        }

        public Movie GetMovieWithComments(int id)
        {
            return Context.Movies.Include(m => m.Comments).Where(m => m.ID == id).FirstOrDefault();
        }
    }
}
