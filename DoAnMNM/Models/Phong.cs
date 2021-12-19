namespace DoAnMNM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Phong")]
    public partial class Phong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phong()
        {
            HoaDonDiens = new HashSet<HoaDonDien>();
            SinhViens = new HashSet<SinhVien>();
        }

        [Key]
        [StringLength(10)]
        public string MaPhong { get; set; }

        [Required]
        [StringLength(20)]
        public string TenPhong { get; set; }

        [Required]
        [StringLength(10)]
        public string MaLoaiPhong { get; set; }

        [Required]
        [StringLength(10)]
        public string MaQL { get; set; }

        [Required]
        [StringLength(10)]
        public string MaKhu { get; set; }

        [StringLength(10)]
        public string MaTruongPhong { get; set; }

        public int? SoDien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonDien> HoaDonDiens { get; set; }

        public virtual Khu Khu { get; set; }

        public virtual LoaiPhong LoaiPhong { get; set; }

        public virtual QuanLy QuanLy { get; set; }

        public virtual SinhVien SinhVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SinhVien> SinhViens { get; set; }

        public override string ToString()
        {
            return TenPhong;
        }
    }
}
