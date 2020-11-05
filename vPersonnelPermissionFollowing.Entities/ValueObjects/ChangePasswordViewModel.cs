using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelPermissionFollowing.Entities.ValueObjects
{
    public class ChangePasswordViewModel
    {
        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."), DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."), DataType(DataType.Password)
            , Compare("Password", ErrorMessage = "{0} ile {1} alanı uyuşmuyor")]
        public string RePassword { get; set; }
    }
}
