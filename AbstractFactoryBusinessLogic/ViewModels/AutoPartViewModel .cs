using System.ComponentModel;

namespace AbstractFactoryBusinessLogic.ViewModels
{
    // Компонент, требуемый для изготовления изделия
    public class AutoPartViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название компонента")]
        public string AutoPartName { get; set; }
    }
}
