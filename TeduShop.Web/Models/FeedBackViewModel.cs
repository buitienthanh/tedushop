using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class FeedBackViewModel
    {
        public int ID { set; get; }
       
        [MaxLength(250,ErrorMessage ="Ten ko dc qua 250 ky tu")]
        [Required(ErrorMessage ="Phai nhap ten")]
        public string Name { set; get; }

        [MaxLength(250, ErrorMessage = "Ten ko dc qua 250 ky tu")]
        public string Email { set; get; }

        [MaxLength(500, ErrorMessage = "Ten ko dc qua 500 ky tu")]
        public string Message { set; get; }

        public DateTime CreatedDate { set; get; }

        [Required(ErrorMessage = "Phai nhap trang thai")]
        public bool Status { set; get; }

        public ContactDetailViewModel ContactDetail { set; get; }
    }
}