using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeoCare.Models
{
    [Table("TBR_PaymentsDetails")]
    public class PaymentsDetails
    {
        [Key]
        public string Pay_ID { get; set; }
        [Column(TypeName = "uniqueidentifier")]
        public Guid OrderID { get; set; }
        public string rz_OrderID { get; set; }
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public PaymentStatus Status { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdate { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
    }

    //public enum PaymentStatus
    //{
    //    Created,
    //    Authorized,
    //    Captured,
    //    Refunded,
    //    Failed,
    //}
    //public enum RefundStatus
    //{
    //    Processing,
    //    Processed,
    //    Failed
    //}
}
