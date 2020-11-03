using AbstractFactoryBusinessLogic.Enums;
using AbstractFactoryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AbstractFactoryFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string AutoPartFileName = "AutoPart.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string ProductFileName = "Product.xml";
        private readonly string ProductAutoPartFileName = "ProductAutoPart.xml";
        public List<AutoPart> AutoParts { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductAutoPart> ProductAutoParts { get; set; }
        private FileDataListSingleton()
        {
            AutoParts = LoadAutoParts();
            Orders = LoadOrders();
            Products = LoadProducts();
            ProductAutoParts = LoadProductAutoParts();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        ~FileDataListSingleton()
        {
            SaveAutoParts();
            SaveOrders();
            SaveProducts();
            SaveProductAutoParts();
        }
        private List<AutoPart> LoadAutoParts()
        {
            var list = new List<AutoPart>();
            if (File.Exists(AutoPartFileName))
            {
                XDocument xDocument = XDocument.Load(AutoPartFileName);
                var xElements = xDocument.Root.Elements("AutoPart").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new AutoPart
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        AutoPartName = elem.Element("AutoPartName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ProductId = Convert.ToInt32(elem.Element("ProductId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus),
                   elem.Element("Status").Value),
                        DateCreate =
                   Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement =
                   string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null :
                   Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }
        private List<Product> LoadProducts()
        {
            var list = new List<Product>();
            if (File.Exists(ProductFileName))
            {
                XDocument xDocument = XDocument.Load(ProductFileName);
            var xElements = xDocument.Root.Elements("Product").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Product
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ProductName = elem.Element("ProductName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }
            return list;
        }
        private List<ProductAutoPart> LoadProductAutoParts()
        {
            var list = new List<ProductAutoPart>();
            if (File.Exists(ProductAutoPartFileName))
            {
                XDocument xDocument = XDocument.Load(ProductAutoPartFileName);
                var xElements = xDocument.Root.Elements("ProductAutoPart").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new ProductAutoPart
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ProductId = Convert.ToInt32(elem.Element("ProductId").Value),
                        AutoPartId = Convert.ToInt32(elem.Element("AutoPartId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        private void SaveAutoParts()
        {
            if (AutoParts != null)
            {
                var xElement = new XElement("AutoParts");
                foreach (var AutoPart in AutoParts)
                {
                    xElement.Add(new XElement("AutoPart",
                    new XAttribute("Id", AutoPart.Id),
                    new XElement("AutoPartName", AutoPart.AutoPartName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(AutoPartFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
            var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("ProductId", order.ProductId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveProducts()
        {
            if (Products != null)
            {
                var xElement = new XElement("Products");
                foreach (var product in Products)
                {
                    xElement.Add(new XElement("Product",
                    new XAttribute("Id", product.Id),
                    new XElement("ProductName", product.ProductName),
                    new XElement("Price", product.Price)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ProductFileName);
            }
        }
        private void SaveProductAutoParts()
        {
            if (ProductAutoParts != null)
            {
                var xElement = new XElement("ProductAutoParts");
                foreach (var productAutoPart in ProductAutoParts)
                {
                    xElement.Add(new XElement("ProductAutoPart",
                    new XAttribute("Id", productAutoPart.Id),
                    new XElement("ProductId", productAutoPart.ProductId),
                    new XElement("AutoPartId", productAutoPart.AutoPartId),
                    new XElement("Count", productAutoPart.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ProductAutoPartFileName);
            }
        }
    }
}
