using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheaterClubProject.Models
{
    [Table("TblOyuncu")]
    public partial class TblOyuncu
    {
        [Key]
        [Column("OyuncuID")]
        [DisplayName("Oyuncu ID")]
        public int OyuncuId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        [DisplayName("Oyuncunun Fotoğrafı")]
        public string? OyuncuFoto { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Oyuncu Ad Soyad")]
        public string? OyuncuAdSoyad { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Oyuncu Tel No")]
        public string? OyuncuTelNo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Oyuncu Email")]
        public string? OyuncuEmail { get; set; }
        [Column(TypeName = "date")]
        [DisplayName("Oyuncunun Doğum Tarihi")]
        public DateTime? OyuncuDogumTarihi { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("Oyuncunun Doğum Yeri")]
        public string? OyuncuDogumYeri { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        [DisplayName("Oyuncunun Mezun Olduğu Üniversite")]
        public string? OyuncuMezunUni { get; set; }
        [NotMapped]
        [DisplayName("Oyun Fotoğrafını Ekle")]
        public IFormFile ImageFile { get; set; }
    }
}
