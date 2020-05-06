namespace ChuoiCuaHangTraSua.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhanHoi")]
    public partial class PhanHoi
    {
        public int Id { get; set; }

        public string NoiDung { get; set; }

        public DateTime? ThoiGian { get; set; }

        public int? MaKhachHang { get; set; }

        public int? DaXem { get; set; }
    }
}
