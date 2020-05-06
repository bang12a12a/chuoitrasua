using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChuoiCuaHangTraSua.Models.Model;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Dao;
namespace ChuoiCuaHangTraSua.Models.Dao
{
    public class NguyenLieuDao
    {
        TraSuaEntities db = null;
        public NguyenLieuDao()
        {
            db = new TraSuaEntities();
        }
        public NguyenLieu getById(int? manguyenlieu)
        {
            return db.NguyenLieux.FirstOrDefault(x => x.Id == manguyenlieu);
        }
        
        
    }
}