using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeoCare.Models
{
    [Table("TBL_Orders")]
    public class Order
    {
        [Key]
        [Column(TypeName = "uniqueidentifier")]
        public Guid OrderID { get; set; }

        [Column(TypeName = "VARCHAR(20)"), Required]
        public string UFID { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual PersonalDetails UserDetails { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus Status { get; set; }

        [Column(TypeName = "VARCHAR(30)"), MaxLength(30)]
        public string CourierPartner { get; set; }

        [Column(TypeName = "VARCHAR(30)"), MaxLength(30)]
        public string TrackingID { get; set; }

        [Column(TypeName = "money")]
        public decimal ShippingFee { get; set; }

        [Column(TypeName = "money"), Required]
        public decimal OrderTotal { get; set; }




        [NotMapped]
        public dropdownhelper drpdown { get; set; }

        [NotMapped]
        public virtual IList<OrderList> orderList { get; set; }
    }

    public class dropdownhelper
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        ReadytoShip,
        Shipped,
        Deliverd,
        Returned
    }

}
