using AbstractFactoryBusinessLogic.BindingModels;
using AbstractFactoryBusinessLogic.HelperModels;
using AbstractFactoryBusinessLogic.Interfaces;
using AbstractFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractFactoryBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IAutoPartLogic AutoPartLogic;
        private readonly IProductLogic ProductLogic;
        private readonly IOrderLogic orderLogic;
        public ReportLogic(IProductLogic ProductLogic, IAutoPartLogic AutoPartLogic,
       IOrderLogic orderLLogic)
        {
            this.ProductLogic = ProductLogic;
            this.AutoPartLogic = AutoPartLogic;
            this.orderLogic = orderLLogic;
        }
        public List<ReportProductAutoPartViewModel> GetProductAutoPart()
        {
            var Products = ProductLogic.Read(null);
            var list = new List<ReportProductAutoPartViewModel>();
            foreach (var product in Products)
            {
                foreach (var pa in product.ProductAutoParts)
                {
                    var record = new ReportProductAutoPartViewModel
                    {
                        ProductName = product.ProductName,
                        AutoPartName = pa.Value.Item1,
                        TotalCount = pa.Value.Item2
                    };
                    list.Add(record);
                }
            }
            return list;
        }
        public List<IGrouping<DateTime, OrderViewModel>> GetOrders(ReportBindingModel model)
        {
            var list = orderLogic
            .Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .GroupBy(rec => rec.DateCreate.Date)
            .OrderBy(recG => recG.Key)
            .ToList();

            return list;
        }
        public void SaveAutoPartsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                AutoParts = AutoPartLogic.Read(null)
            });
        }
        public void SaveOrdersAutoPartToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                Orders = GetOrders(model)
            });
        }
        public void SaveProductsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список закусок по продуктам",
                ProductAutoParts = GetProductAutoPart(),
            });
        }
    }
}