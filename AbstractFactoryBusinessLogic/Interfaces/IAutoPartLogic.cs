using System.Collections.Generic;
using AbstractFactoryBusinessLogic.BindingModels;
using AbstractFactoryBusinessLogic.ViewModels;

namespace AbstractFactoryBusinessLogic.Interfaces
{
    public interface IAutoPartLogic
    {
        List<AutoPartViewModel> Read(AutoPartBindingModel model);

        void CreateOrUpdate(AutoPartBindingModel model);

        void Delete(AutoPartBindingModel model);
    }
}
