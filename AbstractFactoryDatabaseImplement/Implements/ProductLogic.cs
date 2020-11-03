using AbstractFactoryBusinessLogic.BindingModels;
using AbstractFactoryBusinessLogic.Interfaces;
using AbstractFactoryBusinessLogic.ViewModels;
using AbstractFactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractFactoryDatabaseImplement.Implements
{
    public class ProductLogic : IProductLogic
    {
        public void CreateOrUpdate(ProductBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Product element = context.Products.FirstOrDefault(rec =>
                       rec.ProductName == model.ProductName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть изделие с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Products.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Product();
                            context.Products.Add(element);
                        }
                        element.ProductName = model.ProductName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var productAutoParts = context.ProductAutoParts.Where(rec
                           => rec.ProductId == model.Id.Value).ToList();
                            context.ProductAutoParts.RemoveRange(productAutoParts.Where(rec =>
                            !model.ProductAutoParts.ContainsKey(rec.AutoPartId)).ToList());
                            context.SaveChanges();
                            foreach (var updateAutoPart in productAutoParts)
                            {
                                updateAutoPart.Count =
                               model.ProductAutoParts[updateAutoPart.AutoPartId].Item2;

                                model.ProductAutoParts.Remove(updateAutoPart.AutoPartId);
                            }
                            context.SaveChanges();
                        }
                        foreach (var pc in model.ProductAutoParts)
                        {
                            context.ProductAutoParts.Add(new ProductAutoPart
                            {
                                ProductId = element.Id,
                                AutoPartId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(ProductBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.ProductAutoParts.RemoveRange(context.ProductAutoParts.Where(rec =>
                        rec.ProductId == model.Id));
                        Product element = context.Products.FirstOrDefault(rec => rec.Id
                       == model.Id);
                        if (element != null)
                        {
                            context.Products.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public List<ProductViewModel> Read(ProductBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                return context.Products
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new ProductViewModel
               {
                   Id = rec.Id,
                   ProductName = rec.ProductName,
                   Price = rec.Price,
                   ProductAutoParts = context.ProductAutoParts
                .Include(recPC => recPC.AutoPart)
               .Where(recPC => recPC.ProductId == rec.Id)
               .ToDictionary(recPC => recPC.AutoPartId, recPC =>
                (recPC.AutoPart?.AutoPartName, recPC.Count))
               })
               .ToList();
            }
        }
    }
}
