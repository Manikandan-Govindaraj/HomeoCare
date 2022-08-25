using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeoCare.Models
{
    [Table("TBR_OrderList")]
    public class OrderList
    {
        [Key, Column(TypeName = "UNIQUEIDENTIFIER")]
        public Guid RowID { get; set; }

        [Column(TypeName = "UNIQUEIDENTIFIER")]
        public Guid OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Refunded
    }
}
