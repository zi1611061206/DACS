using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiCoffe.DTO
{
    public class ShowBillDTO
    {
        private string tenDoUong;
        private int soLuong;
        private float donGia;
        private float thanhTien;

        public string TenDoUong { get => tenDoUong; set => tenDoUong = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public float DonGia { get => donGia; set => donGia = value; }
        public float ThanhTien { get => thanhTien; set => thanhTien = value; }

        public ShowBillDTO(string tenDoUong, int soLuong, float donGia, float thanhTien)
        {
            this.TenDoUong = tenDoUong;
            this.SoLuong = soLuong;
            this.DonGia = donGia;
            this.ThanhTien = thanhTien;
        }

        public ShowBillDTO(DataRow row)
        {
            this.TenDoUong = row["tendouong"].ToString();
            this.SoLuong = (int)row["soluong"];
            this.DonGia = (float)Convert.ToDouble(row["giaban"].ToString());
            this.ThanhTien = (float)Convert.ToDouble(row["thanhtien"].ToString());
        }
    }
}
