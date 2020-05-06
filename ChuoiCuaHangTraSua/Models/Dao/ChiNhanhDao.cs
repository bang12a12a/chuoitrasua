using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChuoiCuaHangTraSua.Models.Model;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Dao;
namespace ChuoiCuaHangTraSua.Models.Dao
{
    public class ChiNhanhDao
    {
        TraSuaEntities db = new TraSuaEntities();
        public void Insert(ChiNhanh chinhanh)
        {
            db.ChiNhanhs.Add(chinhanh);
            db.SaveChanges();
        }
    }
}