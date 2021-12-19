namespace DoAnMNM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonDien")]
    public partial class HoaDonDien
    {
        [Key]
        public int MaHoaDonDien { get; set; }

        [Required]
        [StringLength(10)]
        public string MaQL { get; set; }

        [Required]
        [StringLength(10)]
        public string MaPhong { get; set; }

        public int SoDienSuDung { get; set; }

        public decimal DonGiaDien { get; set; }

        public int? Thang { get; set; }

        public int? Nam { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayLap { get; set; }

        public bool TinhTrang { get; set; }

        public virtual Phong Phong { get; set; }

        public virtual QuanLy QuanLy { get; set; }
    }
}
