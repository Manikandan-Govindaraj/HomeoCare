using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class ApplicationUser : IdentityUser<Guid>
{
    [Display(Name = "Display Name")]
    [Column(TypeName = "VARCHAR(30)"), Required, MaxLength(30)]
    public string DisplayName { get; set; }
}

//public class Role : IdentityRole<Guid> {
//    public Role(string message) : base(message)
//    {
//        //other stuff here
//    }

    
//}
