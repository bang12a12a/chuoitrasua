namespace ChuoiCuaHangTraSua.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHDB")]
    public partial class ChiTietHDB
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string MaHDB { get; set; }

        public int? MaSanPham { get; set; }

        public int? SoLuong { get; set; }

        public int? GiamGia { get; set; }

        public int? ThanhTien { get; set; }

        public int? ThuocSanPham { get; set; }

        public int? ChiTietThu { get; set; }
    }
}
