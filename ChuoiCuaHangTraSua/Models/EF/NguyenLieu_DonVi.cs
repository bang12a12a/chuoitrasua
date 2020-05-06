namespace ChuoiCuaHangTraSua.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NguyenLieu_DonVi
    {
        public int Id { get; set; }

        public int? MaNguyenLieu { get; set; }

        public int? MaDonViTinh { get; set; }

        public int? SoLuong { get; set; }

        public int? GiaNhap { get; set; }
    }
}
