using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class LoginVM : IValidatableObject
    {
        [Required]
        [DisplayName("帳號")]
        [MinLength(3, ErrorMessage = ("帳號不得少於三位"))]
        public string Username { get; set; }

        [Required]
        [DisplayName("密碼")]
        [MinLength(6, ErrorMessage =("密碼不得低於六位"))]
        public string Password { get; set; }

        public bool LoginCheck()
        {
            return (this.Username == "fish" && this.Password == "123456");
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!this.LoginCheck())
            {
                yield return new ValidationResult("登入失敗", new String[] { "Username" });
                yield break;
            }
            yield return ValidationResult.Success;
        }
    }
}