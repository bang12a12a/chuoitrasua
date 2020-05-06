using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChuoiCuaHangTraSua.Common
{
    public class ConvertMoney
    {
        public int? ConvertTien(string money)
        {
           
            string[] money_split = money.Split(',');
            string lstmoney = "";
            foreach(var item in money_split)
            {
                lstmoney += item;
            }
            int? tien = Convert.ToInt32(lstmoney);    
            return tien;
        }
    }
}