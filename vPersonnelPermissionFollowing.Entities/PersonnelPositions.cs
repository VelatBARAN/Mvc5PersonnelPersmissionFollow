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
    [Table("PersonnelPositions")]
    public class PersonnelPositions : MyEntityBase
    {
        [DisplayName("Personel Unvanı"),Required(ErrorMessage ="{0} alanı boş geçilemez."),StringLength(100,ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        public virtual List<Personnels> Personnels { get; set; }

        public PersonnelPositions()
        {
            Personnels = new List<Personnels>();
        }
    }
}
