using System.Collections.Generic;
using System.Linq;
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
            return View(movies.Select(m => new MovieViewModel { ID = m.ID, Title = m.Title, Genre = m.Genre, Price = m.Price, ReleaseDate = m.ReleaseDate }));
        }

        public IActionResult Create()
        {
            var model = new MovieViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _unitOfWork.MovieRepository.Get(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            var model = new MovieViewModel { ID = movie.ID, Genre = movie.Genre, Price = movie.Price, ReleaseDate = movie.ReleaseDate, Title = movie.Title };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MovieViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var movie = _unitOfWork.MovieRepository.Get(model.ID);
                    movie.ID = model.ID;
                    movie.ReleaseDate = model.ReleaseDate;
                    movie.Genre = model.Genre;
                    movie.Price = model.Price;
                    movie.Title = model.Title;
                    _unitOfWork.MovieRepository.Update(movie);
                    _unitOfWork.Complete();
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _unitOfWork.MovieRepository.Get(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            MovieViewModel model = new MovieViewModel();
            model.ID = movie.ID;
            model.Genre = movie.Genre;
            model.Price = movie.Price;
            model.ReleaseDate = movie.ReleaseDate;
            model.Title = movie.Title;

            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _unitOfWork.MovieRepository.SingleOrDefault(m => m.ID == id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            MovieViewModel model = new MovieViewModel();
            model.ID = movie.ID;
            model.Genre = movie.Genre;
            model.Price = movie.Price;
            model.ReleaseDate = movie.ReleaseDate;
            model.Title = movie.Title;

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _unitOfWork.MovieRepository.SingleOrDefault(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            _unitOfWork.MovieRepository.Remove(movie);
            _unitOfWork.Complete();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
