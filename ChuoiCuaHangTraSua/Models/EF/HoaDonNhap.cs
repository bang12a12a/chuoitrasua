namespace ChuoiCuaHangTraSua.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonNhap")]
    public partial class HoaDonNhap
    {
        [StringLength(50)]
        public string Id { get; set; }

        public int? MaNhanVien { get; set; }

        public int? MaNCC { get; set; }

        public DateTime? NgayNhap { get; set; }

        public int? TongTien { get; set; }

        public int? ChietKhau { get; set; }

        public int? DaThanhToan { get; set; }
    }
}
