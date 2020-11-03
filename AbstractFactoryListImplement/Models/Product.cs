using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryListImplement.Models
{
    // Изделие, изготавливаемое на автозаводе
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
