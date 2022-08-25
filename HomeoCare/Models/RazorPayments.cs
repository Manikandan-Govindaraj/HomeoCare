using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeoCare.Models
{
    [Table("TBL_RazorPayments")]
    public class RazorPayments
    {
        [Column(TypeName = "uniqueidentifier")]
        [Key]
        public Guid UserID { get; set; }

        [Column(TypeName = "VARCHAR(30)"), Required, MaxLength(30)]
        public string OrderID { get; set; }

        [Column(TypeName = "VARCHAR(30)"), MaxLength(30)]
        public string PaymentID { get; set; }

        [Column(TypeName = "VARCHAR(70)"), Required, MaxLength(30)]
        public string Signature { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Column(TypeName = "VARCHAR(5)"), Required, MaxLength(5)]
        public string Currency { get; set; }

        [Column(TypeName = "VARCHAR(40)"), Required, MaxLength(40)]
        public string ReceiptID { get; set; }

        [Column(TypeName = "VARCHAR(256)"), MaxLength(256)]
        public string Notes { get; set; }
    }
}
