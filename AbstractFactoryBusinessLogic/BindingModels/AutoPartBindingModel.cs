using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryBusinessLogic.BindingModels
{
    // Компонент, требуемый для изготовления изделия 
    public class AutoPartBindingModel
    {
        public int ?Id { get; set; }

        public string AutoPartName { get; set; }
    }
}
