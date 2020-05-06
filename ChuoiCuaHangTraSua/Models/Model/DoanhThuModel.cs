using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ChuoiCuaHangTraSua.Models.Model
{
   
    [DataContract]
    public class DoanhThuModel
    {
        public DoanhThuModel(string label, double y)
        {
            this.Label = label;
            this.Y = y;
        }
        public DoanhThuModel()
        {
            this.Label ="";
            this.Y = null;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public string Label = "";

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;

        public int SoLuongBan { set; get; }

        public string NgayBan { set; get; }
    }
}