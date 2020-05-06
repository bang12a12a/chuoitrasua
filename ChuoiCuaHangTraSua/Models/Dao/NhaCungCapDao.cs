using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChuoiCuaHangTraSua.Models.Dao;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Model;

namespace ChuoiCuaHangTraSua.Models.Dao
{
    public class NhaCungCapDao
    {
        TraSuaEntities db = null;
        public NhaCungCapDao()
        {
            db = new TraSuaEntities();

        }
        public NhaCungCap getById(int? mancc)
        {
            return db.NhaCungCaps.FirstOrDefault(x => x.Id == mancc);
        }
    }
}