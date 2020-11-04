using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AbstractFactoryBusinessLogic.BindingModels;
using AbstractFactoryBusinessLogic.Interfaces;
using AbstractFactoryBusinessLogic.ViewModels;
using AbstractFactoryBusinessLogic.BusinessLogics;
using AbstractFactoryRestApi.Models;

namespace AbstractFactoryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IProductLogic _Product;
        private readonly MainLogic _main;
        public MainController(IOrderLogic order, IProductLogic Product, MainLogic main)
        {
            _order = order;
            _Product = Product;
            _main = main;
        }
        [HttpGet]
        public List<ProductModel> GetProductList() => _Product.Read(null)?.Select(rec =>
      Convert(rec)).ToList();
        [HttpGet]
        public ProductModel GetProduct(int ProductId) => Convert(_Product.Read(new
       ProductBindingModel
        { Id = ProductId })?[0]);
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new
       OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _main.CreateOrder(model);
        private ProductModel Convert(ProductViewModel model)
        {
            if (model == null) return null;
            return new ProductModel
            {
                Id = model.Id,
                ProductName = model.ProductName,
                Price = model.Price
            };
        }
    }
}