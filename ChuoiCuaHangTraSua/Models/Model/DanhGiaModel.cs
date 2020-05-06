﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChuoiCuaHangTraSua.Models.Model
{
    public class DanhGiaModel
    {
        public int Id { set; get; }

        public int? MaSanPham { set; get; }

        public string TenDangNhap { set; get; }

        public int? MaKhachHang { set; get; }

        public string NoiDung { set; get; }

        public int? DanhGia { set; get; }

        public string ThoiGian { set; get; }
    }
}