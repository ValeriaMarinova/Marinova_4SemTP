using System.Collections.Generic;
using AbstractFactoryBusinessLogic.BindingModels;
using AbstractFactoryBusinessLogic.ViewModels;


namespace AbstractFactoryBusinessLogic.Interfaces
{
    public interface IProductLogic
    {
        List<ProductViewModel> Read(ProductBindingModel model);

        void CreateOrUpdate(ProductBindingModel model);

        void Delete(ProductBindingModel model);
    }
}
