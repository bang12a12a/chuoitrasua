using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Dao;
namespace ChuoiCuaHangTraSua.Models.Dao
{
    public class HoaDonBanDao
    {
        TraSuaEntities db = null;
        public HoaDonBanDao()
        {
            db = new TraSuaEntities();
        }
        public int tongsoluong(string mahoadonban)
        {
            var tongsoluong =0;
            var list = db.ChiTietHDBs.Where(x => x.MaHDB == mahoadonban);
            foreach (var item in list)
            {
                if(new CategoryDao().getSPChinh(item.MaSanPham) == 1)
                {
                    tongsoluong += 1;
                }
            }
            return tongsoluong;
        }

    }
}