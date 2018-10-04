using System.Collections.Generic;
using testing_net.Models;

namespace testing_net.Repositories.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        IEnumerable<string> GetGenres();
        Movie GetMovieWithComments(int id);
    }
}
