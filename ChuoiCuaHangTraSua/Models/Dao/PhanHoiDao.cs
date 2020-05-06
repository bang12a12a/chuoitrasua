using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChuoiCuaHangTraSua.Models.EF;
namespace ChuoiCuaHangTraSua.Models.Dao
{
    public class PhanHoiDao
    {
        TraSuaEntities db = null;
        public PhanHoiDao()
        {
            db = new TraSuaEntities();
        }
        public PhanHoi getById(int? maphanhoi)
        {
            var phanhoi = db.PhanHois.Find(maphanhoi);
            return phanhoi;
        }
        public void SuaPhanHoi(PhanHoi phanhoi)
        {
            phanhoi.DaXem = 1;
            db.SaveChanges();

        }
    }
}