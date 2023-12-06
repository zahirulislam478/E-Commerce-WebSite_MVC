using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Security.Policy;

namespace E_Commerce_WebSite.Models
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }

    public class Category : EntityBase
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(500)]
        public string Description { get; set; }

        [Required , StringLength(50)]
        public string Picture { get; set; }

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

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class Product : EntityBase
    {
        public string Image { get; set; }
        [Required, StringLength(50)]
        public string Brand { get; set; }
        [Required, StringLength(50)]
        public string ProductName { get; set; }  

        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required]
        public int Rating { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
    public class EcommerceDbContext : DbContext 
    {
        public EcommerceDbContext()
        {
            Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; } 
    }
}