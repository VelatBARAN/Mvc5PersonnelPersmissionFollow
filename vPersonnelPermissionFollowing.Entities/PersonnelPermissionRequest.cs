using PersonnelPermissionFollowing.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelPermissionFollowing.Entities
{
    [Table("PersonnelPermissionRequest")]
    public class PersonnelPermissionRequest : MyEntityBase
    {
        [DisplayName("Personel")]
        public int PersonnelsId { get; set; }

        [DisplayName("İzin Tipi"),Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int PersonnelPermissionTipsId { get; set; }

        [DisplayName("Gün Sayısı"),Required(ErrorMessage ="{0} alanı boş geçilemez.")]
        public int NumberofDays { get; set; }

        [DisplayName("İzin Başlangıç Tarihi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [DataType(DataType.Date)]
        public DateTime? PermissionStartDatetime { get; set; }

        [DisplayName("İzin Bitiş Tarihi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [DataType(DataType.Date)]
        public DateTime? PermissionEndDatetime { get; set; }

        [DisplayName("İzin Durumu")]
        public int PermissionStatesId { get; set; }

        [DisplayName("Kontrol Eden")]
        public int? UsersId { get; set; }

        public virtual Personnels Personnels { get; set; }
        public virtual PersonnelPermissionTips PersonnelPermissionTips { get; set; }
        public virtual PermissionStates PermissionStates { get; set; }
        public virtual Users Users { get; set; }
    }
}
