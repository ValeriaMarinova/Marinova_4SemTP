using System;
using AbstractFactoryBusinessLogic.Enums;

namespace AbstractFactoryBusinessLogic.BindingModels
{
    // Данные от клиента, для создания заказа
    public class CreateOrderBindingModel
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
