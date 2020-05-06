namespace ChuoiCuaHangTraSua.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GioHang")]
    public partial class GioHang
    {
        public int Id { get; set; }

        public int? MaKhachHang { get; set; }

        public int? MaSanPham { get; set; }

        public int? SoLuong { get; set; }

        public int? ThuocSanPham { get; set; }

        public int? SanPhamThu { get; set; }
    }
}
