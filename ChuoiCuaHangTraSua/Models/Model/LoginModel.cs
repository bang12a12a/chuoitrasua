using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChuoiCuaHangTraSua.Models.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập Username")]
        public string Username { set; get; }
        [Required(ErrorMessage = "Mời nhập Password")]
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}