using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChuoiCuaHangTraSua.Models.Model;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Dao;
namespace ChuoiCuaHangTraSua.Models.Dao
{
    public class CTHDNhapDao
    {
        TraSuaEntities db = null;
        public CTHDNhapDao()
        {
            db = new TraSuaEntities();
        }
        public ChiTietHDN findById(int? machitiet)
        {
            return db.ChiTietHDNs.Find(machitiet);
        }
    }
}