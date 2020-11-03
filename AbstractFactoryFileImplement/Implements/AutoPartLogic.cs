using AbstractFactoryBusinessLogic.BindingModels;
using AbstractFactoryBusinessLogic.Interfaces;
using AbstractFactoryBusinessLogic.ViewModels;
using AbstractFactoryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractFactoryFileImplement.Implements
{
    public class AutoPartLogic : IAutoPartLogic
    {
        private readonly FileDataListSingleton source;
        public AutoPartLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(AutoPartBindingModel model)
        {
            AutoPart element = source.AutoParts.FirstOrDefault(rec => rec.AutoPartName
           == model.AutoPartName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть цветок с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.AutoParts.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.AutoParts.Count > 0 ? source.AutoParts.Max(rec =>
               rec.Id) : 0;
                element = new AutoPart { Id = maxId + 1 };
                source.AutoParts.Add(element);
            }
            element.AutoPartName = model.AutoPartName;
        }
        public void Delete(AutoPartBindingModel model)
        {
            AutoPart element = source.AutoParts.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.AutoParts.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<AutoPartViewModel> Read(AutoPartBindingModel model)
        {
            return source.AutoParts
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
