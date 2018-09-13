using System;
<<<<<<< 9b5db0cc73cd9e077e2809922f37d94c6d9acad4
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
<<<<<<< 9b5db0cc73cd9e077e2809922f37d94c6d9acad4

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
        public string Rating { get; set; }

    }
}
