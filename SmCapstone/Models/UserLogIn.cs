using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmCapstone.Models
{
    public class UserLogIn
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Password")]
        public string Psw { get; set; }
    }
}