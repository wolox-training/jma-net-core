using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using testing_net.Repositories.Interfaces;

namespace testing_net.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MoviesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        public IActionResult Index()
        {

            var movies = _unitOfWork.MovieRepository.GetAll();

            return View (movies);
        }

        public IActionResult Create()
        {
            // var viewModel =_unitOfWork.MovieRepository.Add(new Models.Movie())

            // _unitOfWork.Complete();

            return View();
        }
    }
}
