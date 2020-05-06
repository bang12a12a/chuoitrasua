using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChuoiCuaHangTraSua.Common
{
    [Serializable]
    public class NhanVienLogin
    {
        
       
        public int UserId { set; get; }
        public string UserName { set; get; }
        public int MaChiNhanh { set; get; }
    }
}