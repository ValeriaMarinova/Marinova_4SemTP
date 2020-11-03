using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryListImplement.Models
{
    // Сколько компонентов, требуется при изготовлении изделия
    public class ProductAutoPart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AutoPartId { get; set; }
        public int Count { get; set; }
    }
}
