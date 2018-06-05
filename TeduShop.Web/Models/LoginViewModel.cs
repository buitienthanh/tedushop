using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Ban can phai nhap tai khoan")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Ban can phai nhap mat khau")]
        [DataType(DataType.Password)]
        public string Password { set; get; }

        public bool RememberMe { set; get; }
    }
}