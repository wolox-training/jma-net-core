using System;

namespace testing_net.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository MovieRepository { get; }
        ICommentRepository CommentRepository { get; }
        int Complete();       
    }
}
