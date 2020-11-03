using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryFileImplement.Models
{
    public class ProductAutoPart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AutoPartId { get; set; }
        public int Count { get; set; }
    }
}
