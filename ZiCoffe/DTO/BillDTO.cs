using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiCoffe.DTO
{
    public class BillDTO
    {
        private int maHoaDon;
        private int maBan;
        private int trangThai;
        private double giamGia;
        private double tongTien;
        private string ghiChu;

        public int MaHoaDon { get => maHoaDon; set => maHoaDon = value; }
        public int MaBan { get => maBan; set => maBan = value; }
        public int TrangThai { get => trangThai; set => trangThai = value; }
        public double GiamGia { get => giamGia; set => giamGia = value; }
        public double TongTien { get => tongTien; set => tongTien = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }

        public BillDTO(int maHoaDon, int maBan, int trangThai, double giamGia, double tongTien, string ghiChu)
        {
            this.MaHoaDon = maHoaDon;
            this.MaBan = maBan;
            this.TrangThai = trangThai;
            this.GiamGia = giamGia;
            this.TongTien = tongTien;
            this.GhiChu = ghiChu;
        }

        public BillDTO(DataRow row)
        {
            this.MaHoaDon = (int)row["mahoadon"];
            this.MaBan = (int)row["maban"];
            this.TrangThai = Convert.ToInt32(row["trangthai"]);
            this.GiamGia = Convert.ToDouble(row["giamgia"]);
            this.TongTien = Convert.ToDouble(row["tongtien"]);
            this.GhiChu = row["ghichu"].ToString();
        }

    }
}
