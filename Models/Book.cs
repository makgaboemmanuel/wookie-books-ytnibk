using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI.Models
{
    /* this is class data model class */
    public class Book
    {
        /* Each book should have a title, description, author (your custom user model), cover image and price */
        [Key]
        public int Id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string author { get; set; }
        public string cover_image { get; set; }
        [Required]
        public double price { get; set; }
    }
}
