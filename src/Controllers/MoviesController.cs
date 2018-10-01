using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using testing_net.Mail;
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

        public IActionResult Index(string movieGenre, string searchString, string sortOrder, int? page, string currentFilter)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["ReleaseDateSortParm"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["GenreSortParm"] = sortOrder == "genre" ? "genre_desc" : "genre";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
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
            switch (sortOrder)
            {
                case "title_desc":
                    movies = movies.OrderByDescending(m => m.Title);
                    break;
                case "date":
                    movies = movies.OrderBy(m => m.ReleaseDate);
                    break;
                case "date_desc":
                    movies = movies.OrderByDescending(m => m.ReleaseDate);
                    break;
                case "genre":
                    movies = movies.OrderBy(m => m.Genre);
                    break;
                case "genre_desc":
                    movies = movies.OrderByDescending(m => m.Genre);
                    break;
                default:
                    movies = movies.OrderBy(m => m.Title);
                    break;
            }
            var movieGenreVM = new MovieGenreViewModel();
            movieGenreVM.CurrentFilter = searchString;
            movieGenreVM.CurrentSortOrder = sortOrder;
            movieGenreVM.CurrentMovieGenre = movieGenre;
            var movieVMs = movies.Select(m => new MovieViewModel { ID = m.ID, Title = m.Title, ReleaseDate = m.ReleaseDate, Genre = m.Genre, Price = m.Price, Rating = m.Rating }).ToList();
            int pageSize = 3;
            movieGenreVM.Movies = PaginatedList<MovieViewModel>.Create(movieVMs, page ?? 1, pageSize);
            movieGenreVM.Genres = new List<SelectListItem>();
            foreach (var m in movies)
            {
                SelectListItem selectListItem = new SelectListItem() { Text = m.Genre, Value = m.Genre };
                if (!movieGenreVM.Genres.Any(l => l.Value == selectListItem.Value))
                {
                    movieGenreVM.Genres.Add(selectListItem);
                }
            }
            movieGenreVM.Genres = movieGenreVM.Genres.Distinct().ToList();
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
                movie.Comments = model.Comments;
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
            MovieViewModel model = new MovieViewModel { ID = movie.ID, Genre = movie.Genre, Price = movie.Price, ReleaseDate = movie.ReleaseDate, Title = movie.Title, Rating = movie.Rating, Comments = movie.Comments };
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
                    movie.Comments = model.Comments;
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
            MovieViewModel model = new MovieViewModel { ID = movie.ID, Genre = movie.Genre, Price = movie.Price, ReleaseDate = movie.ReleaseDate, Title = movie.Title, Rating = movie.Rating, Comments = movie.Comments };
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
            MovieViewModel model = new MovieViewModel { ID = movie.ID, Genre = movie.Genre, Price = movie.Price, ReleaseDate = movie.ReleaseDate, Title = movie.Title, Rating = movie.Rating, Comments = movie.Comments };
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

        public IActionResult SendMovieToAddress(int? id)
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
            MovieViewModel model = new MovieViewModel { ID = movie.ID, Genre = movie.Genre, Price = movie.Price, ReleaseDate = movie.ReleaseDate, Title = movie.Title, Rating = movie.Rating, Comments = movie.Comments };
            return View(model);
        }

        [HttpPost]
        public IActionResult SendMovieToAddress([FromForm] string EmailAddress, int? id)
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
            StringBuilder builder = new StringBuilder();
            builder.Append(movie.Title.ToString()).Append(" \n");
            builder.Append(movie.Genre.ToString()).Append(" \n");
            builder.Append(movie.ReleaseDate.ToString()).Append(" \n");
            builder.Append(movie.Price.ToString()).Append(" \n");
            builder.Append(movie.Rating.ToString()).Append(" \n");
            builder.Append(movie.Comments.ToString()).Append(" \n");
            Mailer.Send(EmailAddress, movie.Title.ToString(), builder.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}
