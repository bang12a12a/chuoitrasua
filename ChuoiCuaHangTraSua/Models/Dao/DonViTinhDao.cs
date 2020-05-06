using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChuoiCuaHangTraSua.Models.Dao;
using ChuoiCuaHangTraSua.Models.EF;
using ChuoiCuaHangTraSua.Models.Model;

namespace ChuoiCuaHangTraSua.Models.Dao
{
    public class DonViTinhDao
    {
        TraSuaEntities db = null;
        public DonViTinhDao()
        {
            db = new TraSuaEntities();
        }
        public DonViTinh getById(int? madonvitinh)
        {
            return db.DonViTinhs.FirstOrDefault(x => x.Id == madonvitinh);
        }
    }
}