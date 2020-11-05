using PersonnelPermissionFollowing.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelPermissionFollowing.Entities
{
    [Table("Users")]
    public class Users : MyEntityBase
    {
        [DisplayName("Personel"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int PersonnelsId { get; set; }

        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Username { get; set; }

        [DisplayName("Email"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."),
            DataType(DataType.EmailAddress,ErrorMessage ="Lütfen geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."),DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır."),DataType(DataType.Password)
            , Compare("Password",ErrorMessage ="{0} ile {1} alanı uyuşmuyor")]
        public string RePassword { get; set; }

        [DisplayName("Yetki Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int AuthorityId { get; set; }

        public virtual Personnels Personnels { get; set; }
        public virtual Authority Authority { get; set; }
        public virtual List<PersonnelPermissionRequest> PersonnelPermissionRequest { get; set; }
        public Users()
        {
            PersonnelPermissionRequest = new List<PersonnelPermissionRequest>();
        }
    
    }
}
