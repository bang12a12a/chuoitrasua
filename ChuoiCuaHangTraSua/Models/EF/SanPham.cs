namespace ChuoiCuaHangTraSua.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        public int Id { get; set; }

        public string TenSanPham { get; set; }

        public int? MaLoaiSanPham { get; set; }

        public int? GiaBan { get; set; }

        public int? KhuyenMai { get; set; }

        public string Anh { get; set; }

        public string MoTa { get; set; }
    }
}
