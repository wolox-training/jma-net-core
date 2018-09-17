using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Index(string movieGenre, string searchString)
        {
            var genres = _unitOfWork.MovieRepository.GetGenres();
            var movies = _unitOfWork.MovieRepository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.Title.ToLower().Contains(searchString.ToLower()));
            }
            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(m => m.Genre == movieGenre);
            }
            var movieGenreVM = new MovieGenreViewModel();
            movieGenreVM.movies = movies.ToList();
            movieGenreVM.genres = new List<SelectListItem>();
            foreach (var m in movies)
            {
                SelectListItem selectListItem = new SelectListItem() { Text = m.Genre, Value = m.Genre };
                if (!movieGenreVM.genres.Any(l => l.Value == selectListItem.Value))
                {
                    movieGenreVM.genres.Add(selectListItem);
                }
            }
            movieGenreVM.genres = movieGenreVM.genres.Distinct().ToList();
            return View(movieGenreVM);
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
                movie.Rating = model.Rating;
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
                    movie.Rating = model.Rating;
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
            MovieViewModel model = new MovieViewModel { ID = movie.ID, Genre = movie.Genre, Price = movie.Price, ReleaseDate = movie.ReleaseDate, Title = movie.Title, Rating = movie.Rating };
            return View(model);
        }

        public IActionResult Delete(int? id)
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
            MovieViewModel model = new MovieViewModel { ID = movie.ID, Genre = movie.Genre, Price = movie.Price, ReleaseDate = movie.ReleaseDate, Title = movie.Title, Rating = movie.Rating };
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
