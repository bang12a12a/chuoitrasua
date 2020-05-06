using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChuoiCuaHangTraSua.Models.Model
{
    public class TestModel
    {
        public int Id { set; get; }

        public DateTime? NgayBD { set; get; }

        public DateTime? NgayKT { set; get; }

        public string ChonNgay { set; get; }
    }
}