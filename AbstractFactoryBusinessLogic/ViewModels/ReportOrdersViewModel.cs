using System;
using AbstractFactoryBusinessLogic.Enums;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace AbstractFactoryBusinessLogic.ViewModels
{
    public class ReportOrdersViewModel
    {
        public DateTime DateCreate { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
    }
}

