namespace DoAnMNM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonDangKy")]
    public partial class DonDangKy
    {
        [Key]
        [StringLength(10)]
        public string MaDonDangKy { get; set; }

        [Required]
        [StringLength(10)]
        public string MaQL { get; set; }

        [Required]
        [StringLength(10)]
        public string MSSV { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayVao { get; set; }

        public int ThoiGian { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayLamDon { get; set; }

        public virtual QuanLy QuanLy { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
