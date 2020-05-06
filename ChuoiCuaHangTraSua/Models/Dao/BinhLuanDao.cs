using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Model;
namespace ChuoiCuaHangTraSua.Models.Dao
{
    public class BinhLuanDao
    {
        TraSuaEntities db = null;
        public BinhLuanDao()
        {
            db = new TraSuaEntities();
        }

        public void Insert(BinhLuan binhluan)
        {
            db.BinhLuans.Add(binhluan);
            db.SaveChanges();
        }
    }
}