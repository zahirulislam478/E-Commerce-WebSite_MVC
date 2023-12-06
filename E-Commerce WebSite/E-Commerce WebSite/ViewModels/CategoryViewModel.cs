using E_Commerce_WebSite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Commerce_WebSite.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(500)]
        public string Description { get; set; }

        [Required]
        public HttpPostedFileBase Picture { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime FeaturedDate { get; set; }

        [Required]
        public decimal AveragePrice { get; set; }

        [Required, StringLength(50)]
        public string TopSellingProduct { get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}