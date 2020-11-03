using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AbstractFactoryDatabaseImplement.Models
{
   public class AutoPart
    {
        public int Id { get; set; }
        [Required]
        public string AutoPartName { get; set; }
        [ForeignKey("AutoPartId")]
        public virtual List<ProductAutoPart> ProductAutoParts { get; set; }
    }
}
