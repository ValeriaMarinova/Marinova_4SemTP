using System;
using System.Collections.Generic;
using AbstractFactoryBusinessLogic.ViewModels;

namespace AbstractFactoryBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<AutoPartViewModel> AutoParts { get; set; }
    }
}
