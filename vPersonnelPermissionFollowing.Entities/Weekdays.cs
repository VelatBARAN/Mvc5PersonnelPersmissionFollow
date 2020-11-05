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
    [Table("Weekdays")]
    public class Weekdays
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Hafta Tatili"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        public List<AssigningTaskOfPersonnel> AssigningTaskOfPersonnel { get; set; }
        public Weekdays()
        {
            AssigningTaskOfPersonnel = new List<AssigningTaskOfPersonnel>();
        }
    }
}
