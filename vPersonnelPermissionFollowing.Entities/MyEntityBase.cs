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
    public class MyEntityBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Kaydeden"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100,ErrorMessage = "{0} alanı max. {1} karakter olmalıdır")]
        [ScaffoldColumn(false)]
        public string ModifiedUsername { get; set; }

        [DisplayName("Kayıt Tarihi"),Required(ErrorMessage ="{0} alanı boş geçilemez.")]
        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime CreatedOnDatetime { get; set; }

        [DisplayName("Güncelleme Tarihi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime ModifiedOnDatetime { get; set; }

    }
}
