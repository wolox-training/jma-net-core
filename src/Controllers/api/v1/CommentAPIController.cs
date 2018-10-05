using Microsoft.AspNetCore.Mvc;
using testing_net.Models;
using testing_net.Repositories.Interfaces;

namespace testing_net.Controllers
{
    [Route("api/v1/[controller]")]
    public class CommentAPIController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentAPIController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpPost("AddComment")]
        public IActionResult AddComment(int? id, string commentText)
        {
            if (id == null)
            {
                return Json(NotFound());
            }
            var movie = _unitOfWork.MovieRepository.GetMovieWithComments(id.Value);
            if (movie == null)
            {
                return  Json(NotFound());
            }
            var comment = new Comment();
            comment.MovieID = id.Value;
            comment.Movie = movie;
            comment.Text = commentText;
            _unitOfWork.CommentRepository.Add(comment);
            _unitOfWork.Complete();
            return Json(new {Text = commentText});
        }
    }
}
