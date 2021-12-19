namespace DoAnMNM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonTienPhong")]
    public partial class HoaDonTienPhong
    {
        [Key]
        public int MaHoaDonTP { get; set; }

        [Required]
        [StringLength(10)]
        public string MaQL { get; set; }

        [Required]
        [StringLength(10)]
        public string MSSV { get; set; }

        public int Quy { get; set; }

        public int Nam { get; set; }

        [Column(TypeName = "money")]
        public decimal ThanhTien { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayLap { get; set; }

        public bool TinhTrang { get; set; }

        public virtual QuanLy QuanLy { get; set; }

        public virtual SinhVien SinhVien { get; set; }
    }
}
