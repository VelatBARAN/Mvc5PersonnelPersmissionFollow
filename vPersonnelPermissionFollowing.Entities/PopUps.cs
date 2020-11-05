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
    [Table("PopUps")]
    public class PopUps : MyEntityBase
    {
        [DisplayName("Başlık"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Title { get; set; }

        [DisplayName("Resim")]
        public string PopUpImage { get; set; }

        [DisplayName("Aktif Mi?")]
        public bool IsActive  { get; set; }
    }
}
