using System.Collections.Generic;
using AbstractFactoryBusinessLogic.BindingModels;
using AbstractFactoryBusinessLogic.ViewModels;

namespace AbstractFactoryBusinessLogic.Interfaces
{
    public interface IOrderLogic
    {
        List<OrderViewModel> Read(OrderBindingModel model);

        void CreateOrUpdate(OrderBindingModel model);

        void Delete(OrderBindingModel model);
    }
}
