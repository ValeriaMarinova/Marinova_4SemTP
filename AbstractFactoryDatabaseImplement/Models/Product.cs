using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractFactoryDatabaseImplement.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public virtual List<ProductAutoPart> ProductAutoPart { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}