using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testing_net.Models.Views
{
    public class MovieViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
