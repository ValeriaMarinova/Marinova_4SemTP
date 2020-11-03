using AbstractFactoryBusinessLogic.BindingModels;
using AbstractFactoryBusinessLogic.Interfaces;
using AbstractFactoryBusinessLogic.ViewModels;
using AbstractFactoryDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractFactoryDatabaseImplement.Implements
{
    public class AutoPartLogic : IAutoPartLogic
    {
        public void CreateOrUpdate(AutoPartBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                AutoPart element = context.AutoParts.FirstOrDefault(rec =>
                rec.AutoPartName == model.AutoPartName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть продукт с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.AutoParts.FirstOrDefault(rec => rec.Id ==
                    model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new AutoPart();
                    context.AutoParts.Add(element);
                }
                element.AutoPartName = model.AutoPartName;
                context.SaveChanges();
            }
        }
        public void Delete(AutoPartBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                AutoPart element = context.AutoParts.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element != null)
                {
                    context.AutoParts.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<AutoPartViewModel> Read(AutoPartBindingModel model)
        {
            using (var context = new AbstractFactoryDatabase())
            {
                return context.AutoParts
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new AutoPartViewModel
                {
                    Id = rec.Id,
                    AutoPartName = rec.AutoPartName
                })
                .ToList();
            }
        }
    }
}
