using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheaterClubProject.Models
{
    [Table("TblOyun")]
    public partial class TblOyun
    {
        public TblOyun()
        {
            TblGosteriTakips = new HashSet<TblGosteriTakip>();
        }

        [Key]
        [Column("OyunID")]
        [DisplayName("Oyun ID")]
        public int OyunId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        [DisplayName("Oyunun Fotoğrafı")]
        public string? OyunFoto { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Oyunun Adı")]
        public string? OyunAd { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Oyunun Türü")]
        public string? OyunTur { get; set; }
        [DisplayName("Oyunun Dakikası")]
        public int? OyunDakika { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Oyunun Yaş Sınırı")]
        public string? OyunYasSiniri { get; set; }
        [NotMapped]
        [DisplayName("Oyun Fotoğrafını Ekle")]
        public IFormFile ImageFile { get; set; }

        [InverseProperty("GosteriOyun")]
        public virtual ICollection<TblGosteriTakip> TblGosteriTakips { get; set; }
    }
}
