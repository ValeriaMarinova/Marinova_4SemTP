using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryListImplement.Models
{
    // Компонент, требуемый для изготовления изделия
    public class AutoPart
    {
        public int Id { get; set; }
        public string AutoPartName { get; set; }
    }
}
