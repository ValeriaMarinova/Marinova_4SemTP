using System;
using System.Collections.Generic;
using System.Text;
using AbstractFactoryBusinessLogic.ViewModels;

namespace AbstractFactoryBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportProductAutoPartViewModel> ProductAutoParts { get; set; }
    }
}