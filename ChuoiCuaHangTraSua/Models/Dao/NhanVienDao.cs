using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChuoiCuaHangTraSua.Models.EF;
namespace ChuoiCuaHangTraSua.Models.Dao
{
    public class NhanVienDao
    {
        TraSuaEntities db = new TraSuaEntities();
        public NhanVien getById(int? id)
        {
            return db.NhanViens.FirstOrDefault(x => x.Id == id);
        }
        public NhanVien getByName(string name)
        {
            return db.NhanViens.FirstOrDefault(x => x.TenDangNhap == name);
        }
        public bool DangNhapNhanVien(string username, string password)
        {
            var result = db.NhanViens.Count(x => x.TenDangNhap == username && x.MatKhau == password);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}