using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChuoiCuaHangTraSua.Models.EF;
namespace ChuoiCuaHangTraSua.Models.Dao
{
    public class ProductDao
    {
        TraSuaEntities db = null;
        public ProductDao()
        {
            db = new TraSuaEntities();
        }
        public List<SanPham> getTopping()
        {
            return db.SanPhams.Where(x => x.MaLoaiSanPham == 1).ToList();
        }
        public List<SanPham> getDuong()
        {
            return db.SanPhams.Where(x => x.MaLoaiSanPham == 13).ToList();
        }
        public List<SanPham> getDa()
        {
            return db.SanPhams.Where(x => x.MaLoaiSanPham == 12).ToList();
        }
        public List<SanPham> getSize()
        {
            return db.SanPhams.Where(x => x.MaLoaiSanPham == 14).ToList();
        }
        public SanPham getByid(int? productid)
        {
            return db.SanPhams.SingleOrDefault(x => x.Id == productid);
        }
        public SanPham viewDetail(int? productid)
        {
            return (db.SanPhams.Find(productid));
        }
        public int Insert(SanPham sanpham)
        {
            db.SanPhams.Add(sanpham);
            db.SaveChanges();
            return sanpham.Id;
        }
        public void Delete(int masanpham)
        {
            var product = db.SanPhams.Find(masanpham);
            db.SanPhams.Remove(product);
            db.SaveChanges();
        }
    }
}