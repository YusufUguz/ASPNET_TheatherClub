using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheaterClubProject.Models
{
    [Table("Sehirilce")]
    public partial class Sehirilce
    {
        public Sehirilce()
        {
            TblGosteriTakips = new HashSet<TblGosteriTakip>();
        }

        [Key]
        [Column("SehirilceId")]
        public int SehirilceId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? SehirilceBilgi { get; set; }
        [Column("SehirId")]
        public int? SehirId { get; set; }
        [ForeignKey("SehirId")]
        [InverseProperty("Sehirilces")]
        public virtual Sehir? Sehir { get; set; }




        [InverseProperty("GosteriSehirilce")]
        public virtual ICollection<TblGosteriTakip> TblGosteriTakips { get; set; }
    }
}
