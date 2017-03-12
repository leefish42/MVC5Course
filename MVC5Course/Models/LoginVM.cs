using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class LoginVM
    {
        [Required]
        [DisplayName("帳號")]
        [MinLength(3, ErrorMessage = ("帳號不得少於三位"))]
        public string Username { get; set; }

        [Required]
        [DisplayName("密碼")]
        [MinLength(6, ErrorMessage =("密碼不得少於六位"))]
        public string Password { get; set; }
    }
}