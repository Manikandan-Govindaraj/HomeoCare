using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeoCare.Models
{
    [Table("TBL_AddressDetails")]
    public class AddressDetails
    {
        [Key]
        public Guid AddressID { get; set; }

        [Column(TypeName = "uniqueidentifier")]
        public Guid UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual PersonalDetails UserDetails { get; set; }

        [Display(Name = "Contact Name")]
        [Column(TypeName = "VARCHAR(30)"), Required,MaxLength(30)]
        public string ContactName { get; set; }

        [Column(TypeName = "VARCHAR(10)"), StringLength(10, ErrorMessage = "The {0} must {1} characters long.", MinimumLength = 10), Required]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Please enter valid mobile number.")]
        public string Mobile { get; set; }

        [Column(TypeName = "VARCHAR(10)"), StringLength(10, ErrorMessage = "The {0} must {1} characters long.", MinimumLength = 10)]
        [Display(Name = "Alternate Phone")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Please enter valid alternate phone.")]
        public string AlternateMobile { get; set; }

        [Display(Name = "Address")]
        [Column(TypeName = "VARCHAR(50)"), Required, MaxLength(50)]
        public string AddressLine1 { get; set; }

        [Column(TypeName = "VARCHAR(30)"), Required, MaxLength(30)]
        public string City { get; set; }

        [Column(TypeName = "VARCHAR(20)"), Required, MaxLength(20)]
        public string State { get; set; }

        [Column(TypeName = "VARCHAR(10)"), Required] 
        public string PinCode { get; set; }

        [Column(TypeName = "VARCHAR(50)"), MaxLength(50)]
        public string Lankmark { get; set; }

        [Display(Name = "Address Type")]
        public AddressType addressType { get; set; }
    }

    public enum AddressType
    {
        Home,
        Work
    }
}
