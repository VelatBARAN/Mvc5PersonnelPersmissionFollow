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
    [Table("PersonnelPermissionTips")]
    public class PersonnelPermissionTips : MyEntityBase
    {
        [DisplayName("İzin Tipi"),Required(ErrorMessage ="{0} alanı boş geçilemez"),StringLength(50,ErrorMessage = "{0} alanı max. {1} karakter olmalıdır")]
        public string Name { get; set; }

        public virtual List<PersonnelPermissionRequest> PersonnelPermissionRequest { get; set; }
        public PersonnelPermissionTips()
        {
            PersonnelPermissionRequest = new List<PersonnelPermissionRequest>();
        }
    }
}
