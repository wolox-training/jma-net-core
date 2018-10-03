using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace testing_net.Models.Views
{
    public class MovieGenreViewModel
    {
        public PaginatedList<MovieViewModel> Movies { get; set; }
        public List<SelectListItem> Genres { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentMovieGenre { get; set; }
        public string CurrentSortOrder { get; set; }
    }
}
