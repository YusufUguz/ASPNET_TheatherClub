using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheaterClubProject.Models
{
    [Table("Sehir")]
    public partial class Sehir
    {
        public Sehir()
        {
            Sehirilces = new HashSet<Sehirilce>();
            TblGosteriTakips = new HashSet<TblGosteriTakip>();
        }

        [Key]
        [Column("SehirId")]
        public int SehirId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? SehirBilgi { get; set; }

        [InverseProperty("Sehir")]
        public virtual ICollection<Sehirilce> Sehirilces { get; set; }


        [InverseProperty("GosteriSehir")]
        public virtual ICollection<TblGosteriTakip> TblGosteriTakips { get; set; }
    }
}
