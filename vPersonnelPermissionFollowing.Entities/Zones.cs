﻿using PersonnelPermissionFollowing.Entities;
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
    [Table("Zones")]
    public class Zones : MyEntityBase
    {
        [DisplayName("Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Amir/Şef"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public int PersonnelsId { get; set; }

        public virtual Personnels Personnels { get; set; }
        public virtual List<AssigningTaskOfPersonnel> AssigningTaskOfPersonnel { get; set; }
        public Zones()
        {
            AssigningTaskOfPersonnel = new List<AssigningTaskOfPersonnel>();
        }
    }
}
