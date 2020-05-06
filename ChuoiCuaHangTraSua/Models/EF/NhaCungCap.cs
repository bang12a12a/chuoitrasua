namespace ChuoiCuaHangTraSua.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhaCungCap")]
    public partial class NhaCungCap
    {
        public int Id { get; set; }

        public string TenNCC { get; set; }

        public string DiaChi { get; set; }

        [StringLength(50)]
        public string DienThoai { get; set; }
    }
}
