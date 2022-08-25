using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeoCare.Models
{
    [Table("TBL_Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(20)"), Required, MaxLength(20)]
        public string Name { get; set; }

        [DisplayName("Display Order"), Required]
        [Range(1, int.MaxValue, ErrorMessage = "Display Order for category must be greater than 0")]
        public int DisplayOrder { get; set; }
    }
}
