using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TheaterClubProject.Models
{
    [Table("TblGosteriTakip")]
    public partial class TblGosteriTakip
    {
        [Key]
        [Column("GosteriID")]
        [DisplayName("Gösteri ID")]
        public int GosteriId { get; set; }
        [StringLength(100)]
        [DisplayName("Gösteri Fotoğrafı")]
        public string? GosteriFoto { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        [DisplayName("Gösteri Adı")]
        public string? GosteriAd { get; set; }
        [Column("GosteriOyunID")]
        [DisplayName("Gösteri Oyunu")]
        public int? GosteriOyunId { get; set; }
        [Unicode(false)]
        [DisplayName("Gösteri Oyuncu Ekibi")]
        public string? GosteriOyuncular { get; set; }
        [Unicode(false)]
        [DisplayName("Gösteri Sahne Arkası Ekibi")]
        public string? GosteriSahneArkasi { get; set; }
        [Column("GosteriSalonID")]
        [DisplayName("Gösteri Salonu")]
        public int? GosteriSalonId { get; set; }
        [Column("GosteriSehirID")]
        [DisplayName("Gösteri Şehir")]
        public int? GosteriSehirId { get; set; }
        [Column("GosteriSehirilceID")]
        [DisplayName("Gösteri İlçe")]
        public int? GosteriSehirilceId { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        [DisplayName("Gösteri Fiyatı")]
        public string? GosteriFiyat { get; set; }
        [Column(TypeName = "datetime")]
        [DisplayName("Gösteri Tarihi")]
        public DateTime? GosteriTarih { get; set; }
        [NotMapped]
        [DisplayName("Gösteri Fotoğrafını Ekle")]
        public IFormFile? ImageFile { get; set; }

        [ForeignKey("GosteriOyunId")]
        [InverseProperty("TblGosteriTakips")]
        public virtual TblOyun? GosteriOyun { get; set; }
        [ForeignKey("GosteriSalonId")]
        [InverseProperty("TblGosteriTakips")]
        public virtual TblGosteriSalonu? GosteriSalon { get; set; }
     
        [ForeignKey("GosteriSehirId")]
        [InverseProperty("TblGosteriTakips")]
        public virtual Sehir? GosteriSehir { get; set; }
        [ForeignKey("GosteriSehirilceId")]
        [InverseProperty("TblGosteriTakips")]
        public virtual Sehirilce? GosteriSehirilce { get; set; }
    }
}
