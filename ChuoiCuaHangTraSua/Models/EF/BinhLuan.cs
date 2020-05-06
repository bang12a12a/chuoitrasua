namespace ChuoiCuaHangTraSua.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        public int Id { get; set; }

        public string NoiDung { get; set; }

        public int? DanhGia { get; set; }

        public int? MaKhachHang { get; set; }

        public int? MaSanPham { get; set; }

        public DateTime? ThoiGian { get; set; }
    }
}
