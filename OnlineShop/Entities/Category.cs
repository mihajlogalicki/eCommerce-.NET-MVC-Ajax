using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(8)]
        public string Name { get; set; }
        [Required]
        [MinLength(10), MaxLength(80)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFeatured { get; set; }
        public List<Product> Products;
    }
}