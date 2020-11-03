using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryBusinessLogic.BindingModels
{
    // Изделие, изготавливаемое в магазине 
    public class ProductBindingModel
    {
        public int? Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> ProductAutoParts { get; set; }
    }
}
