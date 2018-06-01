using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class ContactDetailViewModel
    {
       
        public int ID { set; get; }

        [Required(ErrorMessage ="Tên không dc trống")]
        public string Name { set; get; }

        [MaxLength(50,ErrorMessage = "Tên không dc trống")]
        public string Phone { set; get; }

        [MaxLength(250, ErrorMessage = "Email ko dc vượt quá 50 ký tự")]
        public string Email { set; get; }

        [MaxLength(250, ErrorMessage = "Website ko dc vượt quá 50 ký tự")]
        public string Web { set; get; }

        [MaxLength(250, ErrorMessage = "Address ko dc vượt quá 50 ký tự")]
        public string Address { set; get; }
        public string Others { set; get; }


        public double? Lat { set; get; }
        public double? Lng { set; get; }
        public bool Status { set; get; }
    }
}