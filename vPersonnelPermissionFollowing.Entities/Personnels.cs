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
    [Table("Personnels")]
    public class Personnels : MyEntityBase
    {
        [DisplayName("TC"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(11, ErrorMessage = "{0} alanı min. {1} karakter olmalıdır. {1} karakteri geçemez.",MinimumLength =11)]
        public string Tc { get; set; }

        [DisplayName("Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyadı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("Resim")]
        public string ProfileImage { get; set; }

        [DisplayName("Başlama Tarihi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
       // [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? StartToJobDateTime { get; set; }

        [DisplayName("Çıkış Tarihi")]
       // [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? ExitOfJobDatetime { get; set; }

        [DisplayName("Toplam Çalışma Yılı")]
        [ScaffoldColumn(false)]
        public int TotalWorkingYear { get; set; }

        [DisplayName("Kazanılan İzin")]
        [ScaffoldColumn(false)]
        public int TotalAllowDay { get; set; }

        [DisplayName("Kullanılan İzin")]
        [ScaffoldColumn(false)]
        public int TotalUsingAllowDay { get; set; }

        [DisplayName("Kalan İzin")]
        [ScaffoldColumn(false)]
        public int TotalRemainAllowDay { get; set; }

        [DisplayName("Çalışma Durumu")]
        [ScaffoldColumn(false)]
        public bool IsStateWorking { get; set; }

        [DisplayName("Görev Atandı Mı")]
        [ScaffoldColumn(false)]
        public bool IsAssignedTask { get; set; }

        [DisplayName("Eğitim Durumu"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int PersonnelDegreesId { get; set; }

        [DisplayName("Görevi"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int PersonnelTasksId { get; set; }

        [DisplayName("Unvanı"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public int PersonnelPositionsId { get; set; }

        [DisplayName("Açıklama"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(2000, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Description { get; set; }

        public virtual PersonnelDegrees PersonnelDegrees { get; set; }
        public virtual PersonnelTasks PersonnelTasks { get; set; }
        public virtual PersonnelPositions PersonnelPositions { get; set; }
        public virtual List<AssigningTaskOfPersonnel> AssigningTaskOfPersonnel { get; set; }
        public virtual List<PersonnelPermissionRequest> PersonnelPermissionRequest { get; set; }
        public virtual List<Users> Users { get; set; }
        public virtual List<Zones> Zones { get; set; }

        public Personnels()
        {
            AssigningTaskOfPersonnel = new List<AssigningTaskOfPersonnel>();
            PersonnelPermissionRequest = new List<PersonnelPermissionRequest>();
            Users = new List<Users>();
            Zones = new List<Zones>();
        }
    }
}
