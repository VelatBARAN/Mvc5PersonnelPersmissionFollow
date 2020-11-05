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
    [Table("AssigningTaskOfPersonnel")]
    public class AssigningTaskOfPersonnel : MyEntityBase
    {
        [DisplayName("Mıntıka(Bölge)"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int ZonesId { get; set; }

        [DisplayName("Personel"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int PersonnelsId { get; set; }

        [DisplayName("Hafta Tatili"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int WeekdaysId { get; set; }

        [DisplayName("Görev Açıklama"),Required(ErrorMessage ="{0} alanı boş geçilemez."),StringLength(2000,ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string TaskDescription { get; set; }

        [DisplayName("Telefon"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(11, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Geçersiz telefon numarası")]
        public string Phone { get; set; }

        public virtual Personnels Personnels { get; set; }
        public virtual Zones Zones { get; set; }
        public virtual Weekdays Weekdays { get; set; }

    }
}
