using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheaterClubProject.Models
{
    [Table("TblSA_Alan")]
    public partial class TblSaAlan
    {
        public TblSaAlan()
        {
            TblSahneArkasis = new HashSet<TblSahneArkasi>();
        }

        [Key]
        [Column("SA_AlanID")]
        public int SaAlanId { get; set; }
        [Column("SA_Alan")]
        [StringLength(50)]
        [Unicode(false)]
        public string? SaAlan { get; set; }

        [InverseProperty("SaAlan")]
        public virtual ICollection<TblSahneArkasi> TblSahneArkasis { get; set; }
    }
}
