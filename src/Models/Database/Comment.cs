using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testing_net.Models
{
    public class Comment
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(500)]
        [Required]
        public string Text { get; set; }
        public int MovieID { get; set; }
        public Movie Movie { get; set; }
    }
}
