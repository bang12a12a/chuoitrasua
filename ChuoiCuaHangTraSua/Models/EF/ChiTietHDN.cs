namespace ChuoiCuaHangTraSua.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHDN")]
    public partial class ChiTietHDN
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string MaHDN { get; set; }

        public int? MaNguyenLieu { get; set; }

        public int? MaDonViTinh { get; set; }

        public int? GiaNhap { get; set; }

        public int? SoLuong { get; set; }

        public int? ChietKhau { get; set; }

        public int? ThanhTien { get; set; }
    }
}
