using System.Collections.Generic;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using testing_net.Models;
using testing_net.Models.Views;
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

            var moviesModels = new List<MovieViewModel>();

            foreach (var m in movies)
            {
                var vm = new MovieViewModel { ID = m.ID, Title = m.Title, Genre = m.Genre, Price = m.Price, ReleaseDate = m.ReleaseDate };
                moviesModels.Add(vm);
            }

            return View(moviesModels);
        }

        public IActionResult Create()
        {
            var model = new MovieViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie();
                movie.ReleaseDate = model.ReleaseDate;
                movie.Genre = model.Genre;
                movie.Price = model.Price;
                movie.Title = model.Title;
                _unitOfWork.MovieRepository.Add(movie);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}

