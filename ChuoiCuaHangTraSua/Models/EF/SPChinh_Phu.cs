namespace ChuoiCuaHangTraSua.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SPChinh_Phu
    {
        public int Id { get; set; }

        [Column("SPChinh_Phu")]
        [StringLength(50)]
        public string SPChinh_Phu1 { get; set; }
    }
}
