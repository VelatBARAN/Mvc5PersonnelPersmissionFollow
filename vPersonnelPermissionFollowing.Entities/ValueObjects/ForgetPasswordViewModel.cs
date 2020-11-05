using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelPermissionFollowing.Entities.ValueObjects
{
    public class ForgetPasswordViewModel
    {
        [DisplayName("Email"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."),
    DataType(DataType.EmailAddress, ErrorMessage = "Lütfen geçerli bir email adresi giriniz.")]
        public string Email { get; set; }
    }
}
