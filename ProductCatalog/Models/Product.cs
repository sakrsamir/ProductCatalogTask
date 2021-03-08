using System;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models
{
    public class Product
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public float Price { get; set; }
        public DateTime? LastUpdated { get; set; }

    }
}