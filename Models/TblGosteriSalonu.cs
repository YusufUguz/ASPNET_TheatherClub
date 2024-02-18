using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheaterClubProject.Models
{
    [Table("TblGosteriSalonu")]
    public partial class TblGosteriSalonu
    {
        public TblGosteriSalonu()
        {
            TblGosteriTakips = new HashSet<TblGosteriTakip>();
        }

        [Key]
        [Column("SalonID")]
        [DisplayName("Salon ID")]
        public int SalonId { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        [DisplayName("Salon Adı")]
        public string? SalonAd { get; set; }
        [StringLength(300)]
        [Unicode(false)]
        [DisplayName("Salon Adresi")]
        public string? SalonAdres { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Salon Tel No")]
        public string? SalonTelNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Salon Email")]
        public string? SalonEmail { get; set; }

        [InverseProperty("GosteriSalon")]
        public virtual ICollection<TblGosteriTakip> TblGosteriTakips { get; set; }
    }
}
