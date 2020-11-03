using AbstractFactoryBusinessLogic.BindingModels;
using AbstractFactoryBusinessLogic.Interfaces;
using AbstractFactoryBusinessLogic.ViewModels;
using AbstractFactoryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractFactoryFileImplement.Implements
{
    public class ProductLogic : IProductLogic
    {
        private readonly FileDataListSingleton source;
        public ProductLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(ProductBindingModel model)
        {
            Product element = source.Products.FirstOrDefault(rec => rec.ProductName ==
           model.ProductName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть букет с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Products.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Products.Count > 0 ? source.AutoParts.Max(rec =>
               rec.Id) : 0;
                element = new Product { Id = maxId + 1 };
                source.Products.Add(element);
            }
            element.ProductName = model.ProductName;
            element.Price = model.Price;
            source.ProductAutoParts.RemoveAll(rec => rec.ProductId == model.Id &&
           !model.ProductAutoParts.ContainsKey(rec.AutoPartId));
            var updateAutoParts = source.ProductAutoParts.Where(rec => rec.ProductId ==
           model.Id && model.ProductAutoParts.ContainsKey(rec.AutoPartId));
            foreach (var updateAutoPart in updateAutoParts)
            {
                updateAutoPart.Count =
               model.ProductAutoParts[updateAutoPart.AutoPartId].Item2;
                model.ProductAutoParts.Remove(updateAutoPart.AutoPartId);
            }
            int maxPCId = source.ProductAutoParts.Count > 0 ?
           source.ProductAutoParts.Max(rec => rec.Id) : 0;
            foreach (var pc in model.ProductAutoParts)
            {
                source.ProductAutoParts.Add(new ProductAutoPart
                {
                    Id = ++maxPCId,
                    ProductId = element.Id,
                    AutoPartId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }
        public void Delete(ProductBindingModel model)
        {
            source.ProductAutoParts.RemoveAll(rec => rec.ProductId == model.Id);
            Product element = source.Products.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Products.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<ProductViewModel> Read(ProductBindingModel model)
        {
            return source.Products
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new ProductViewModel
            {
                Id = rec.Id,
                ProductName = rec.ProductName,
                Price = rec.Price,
                ProductAutoParts = source.ProductAutoParts
            .Where(recPC => recPC.ProductId == rec.Id)
           .ToDictionary(recPC => recPC.AutoPartId, recPC =>
            (source.AutoParts.FirstOrDefault(recC => recC.Id ==
           recPC.AutoPartId)?.AutoPartName, recPC.Count))
            })
            .ToList();
        }
    }
}
