using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheaterClubProject.Models
{
    [Table("TblSahneArkasi")]
    [Index("SaAdSoyad", Name = "UQ_SAdSoyad", IsUnique = true)]
    public partial class TblSahneArkasi
    {
        [Key]
        [Column("SahneArkasiID")]
        [DisplayName("SA ID")]
        public int SahneArkasiId { get; set; }
        [Column("SA_Foto")]
        [StringLength(100)]
        [Unicode(false)]
        [DisplayName("SA Fotoğrafı")]
        public string? SaFoto { get; set; }
        [Column("SA_AdSoyad")]
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("SA Ad Soyad")]
        public string? SaAdSoyad { get; set; }
        [Column("SA_TelNo")]
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("SA Tel No")]
        public string? SaTelNo { get; set; }
        [Column("SA_Email")]
        [StringLength(50)]
        [Unicode(false)]
        [DisplayName("SA Email")]
        public string? SaEmail { get; set; }
        [Column("SA_AlanID")]
        [DisplayName("SA Alan")]
        public int? SaAlanId { get; set; }
        [Column("SA_DogumTarihi", TypeName = "date")]
        [DisplayName("SA Doğum Tarihi")]
        public DateTime? SaDogumTarihi { get; set; }

        [ForeignKey("SaAlanId")]
        [InverseProperty("TblSahneArkasis")]
        public virtual TblSaAlan? SaAlan { get; set; }

        [NotMapped]
        [DisplayName("Oyun Fotoğrafını Ekle")]
        public IFormFile ImageFile { get; set; }
    }
}
