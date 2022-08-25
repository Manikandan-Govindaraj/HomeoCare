using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeoCare.Models
{
    public class RazerPayError
    {
        [Column(TypeName = "uniqueidentifier")]
        [Key]
        public Guid UserID { get; set; }

        [Column(TypeName = "VARCHAR(50)"), Required, MaxLength(50)]
        public string code { get; set; }

        [Column(TypeName = "VARCHAR(256)"), Required, MaxLength(256)]
        public string Description { get; set; }

        [Column(TypeName = "VARCHAR(30)"), Required, MaxLength(30)]
        public string OrderID { get; set; }

        [Column(TypeName = "VARCHAR(30)"), Required, MaxLength(30)]
        public string PaymentID { get; set; }

        [Column(TypeName = "VARCHAR(50)"), Required, MaxLength(50)]
        public string Reason { get; set; }

        [Column(TypeName = "VARCHAR(50)"), Required, MaxLength(50)]
        public string Source { get; set; }

        [Column(TypeName = "VARCHAR(50)"), Required, MaxLength(50)]
        public string Step { get; set; }

    }
}
