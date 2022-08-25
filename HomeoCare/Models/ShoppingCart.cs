using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeoCare.Models
{
    [Table("TBL_ShoppingCart")]
    public class ShoppingCart
    {
        [Key]
        public int RowID { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual PersonalDetails UserDetails { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product ProductDetails { get; set; }

        public int Quantity { get; set; }


        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
