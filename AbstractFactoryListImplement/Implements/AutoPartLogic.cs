using AbstractFactoryListImplement.Models;
using System.Collections.Generic;
using AbstractFactoryBusinessLogic.BindingModels;
using AbstractFactoryBusinessLogic.Interfaces;
using AbstractFactoryBusinessLogic.ViewModels;
using System;
namespace AbstractFactoryListImplement.Implements
{
    public class AutoPartLogic : IAutoPartLogic
    {
        private readonly DataListSingleton source;
        public AutoPartLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(AutoPartBindingModel model)
        {
            AutoPart tempAutoPart = model.Id.HasValue ? null : new AutoPart
            {
                Id = 1
            };
            foreach (var AutoPart in source.AutoParts)
            {
                if (AutoPart.AutoPartName == model.AutoPartName && AutoPart.Id !=
               model.Id)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
                if (!model.Id.HasValue && AutoPart.Id >= tempAutoPart.Id)
                {
                    tempAutoPart.Id = AutoPart.Id + 1;
                }
                else if (model.Id.HasValue && AutoPart.Id == model.Id)
                {
                    tempAutoPart = AutoPart;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempAutoPart == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempAutoPart);
            }
            else
            {
                source.AutoParts.Add(CreateModel(model, tempAutoPart));
            }
        }
        public void Delete(AutoPartBindingModel model)
        {
            for (int i = 0; i < source.AutoParts.Count; ++i)
            {
                if (source.AutoParts[i].Id == model.Id.Value)
                {
                    source.AutoParts.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<AutoPartViewModel> Read(AutoPartBindingModel model)
        {
            List<AutoPartViewModel> result = new List<AutoPartViewModel>();
            foreach (var AutoPart in source.AutoParts)
            {
                if (model != null)
                {
                    if (AutoPart.Id == model.Id)
                    {
                        result.Add(CreateViewModel(AutoPart));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(AutoPart));
            }
            return result;
        }
        private AutoPart CreateModel(AutoPartBindingModel model, AutoPart AutoPart)
        {
            AutoPart.AutoPartName = model.AutoPartName;
            return AutoPart;
        }
        private AutoPartViewModel CreateViewModel(AutoPart AutoPart)
        {
            return new AutoPartViewModel
            {
                Id = AutoPart.Id,
                AutoPartName = AutoPart.AutoPartName
            };
        }
    }
}
