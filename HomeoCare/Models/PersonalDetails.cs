using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeoCare.Models
{
    [Table("TBL_PersonalDetails")]
    public class PersonalDetails
    {
        [Column(TypeName = "uniqueidentifier")]
        [Key]
        public Guid UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "First Name")]
        [Column(TypeName = "VARCHAR(30)"), Required, MaxLength(30)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Column(TypeName = "VARCHAR(30)"), Required, MaxLength(30)]
        public string LastName { get; set; }

        [Column(TypeName = "VARCHAR(10)"), StringLength(10, ErrorMessage = "The {0} must {1} characters long.", MinimumLength = 10), Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Please enter valid Phone Number.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [Column(TypeName = "VARCHAR(50)"), Required, MaxLength(50)]
        [EmailAddress(ErrorMessage = "Enter Valid Email Address")]
        public string Email { get; set; }

        [Column(TypeName = "VARCHAR(25)")]
        public string RazorPayID { get; set; }
    }
}
