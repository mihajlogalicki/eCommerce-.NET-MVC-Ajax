using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShop.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(25)]
        public string Name { get; set; }
        [Required]
        [MinLength(3), MaxLength(90)]
        public string Description { get; set; }
        [Range(1, 10000)]
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [ForeignKey("Category")]
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}