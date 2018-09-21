using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace testing_net.Models.Views
{
    public class MovieGenreViewModel
    {
        public List<Movie> movies;
        public List<SelectListItem> genres;
        public string movieGenre { get; set; }
    }
}
