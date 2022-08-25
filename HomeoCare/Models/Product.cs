using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HomeoCare.Models
{
    [Table("TBL_Products")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Column(TypeName = "VARCHAR(20)"), MaxLength(20)]
        public string SKU { get; set; }

        [Column(TypeName = "VARCHAR(50)"), Required, MaxLength(50)]
        public string ProductName { get; set; }

        [Display(Name = "Category Type")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Quantity On Hand")]
        public int QuantityOnHand { get; set; }
   
        [Column(TypeName = "money"), Display(Name = "Original Price")]
        public decimal OriginalPrice { get; set; }

        [Column(TypeName = "money"), Display(Name = "Retail Price")]
        public decimal RetailPrice { get; set; }

        [Column(TypeName = "money"), Required]
        public decimal SellingPrice { get; set; }
     
        [Column(TypeName = "VARCHAR(120)"), MaxLength(120)]
        public string ShortDesc { get; set; }

        [Column(TypeName = "VARCHAR(350)"), MaxLength(350)]
        public string Description { get; set; }

        [Column(TypeName = "VARCHAR(50)"), MaxLength(50)]
        public string ImagePath { get; set; }

        //[Display(Name = "Product Status")]
        //public ProductStatus Status { get; set; }

        [NotMapped]
        public int TempQty { get; set; }




        //[Column(TypeName = "VARCHAR(50)"), MaxLength(50)]
        //public string ModelName { get; set; }
        //[Column(TypeName = "VARCHAR(50)"), MaxLength(50)]
        //public string Brand { get; set; }
        //[Column(TypeName = "VARCHAR(20)"), MaxLength(20)]
        //public string StorageLife { get; set; }
        //[Column(TypeName = "VARCHAR(50)"), MaxLength(50)]
        //public string NutrientContent { get; set; }
        //[Column(TypeName = "VARCHAR(50)"), MaxLength(50)]
        //public string Suitablefor { get; set; }
        //[Column(TypeName = "VARCHAR(50)"), MaxLength(50)]
        //public string StorageInstructions { get; set; }
        //[Column(TypeName = "VARCHAR(100)"), MaxLength(100)]
        //public string ImportantNote { get; set; }



    }

    public enum ProductStatus
    {
        InStock,
        OutofStock,
        Discontinued,
        TemporarilyUnavailable,
    }
}
