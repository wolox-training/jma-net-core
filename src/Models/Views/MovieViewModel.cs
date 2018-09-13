using System;
<<<<<<< 6a10f5dd259dd895bf5d1d86e08aa02da504297f
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
=======
>>>>>>> Create movie endpoint. Connecting UoW  with Controller, View and MovieViewModel.

namespace testing_net.Models.Views
{
    public class MovieViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
<<<<<<< 6a10f5dd259dd895bf5d1d86e08aa02da504297f

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
=======
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
>>>>>>> Create movie endpoint. Connecting UoW  with Controller, View and MovieViewModel.
        public decimal Price { get; set; }
    }
}
