using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiCoffe.DTO
{
    public class BillInfoDTO
    {
        private int maHoaDon;
        //private string tendangnhap;
        private int maDoUong;
        private DateTime? thoiGianLap;
        private DateTime? thoiGianKet;
        private int soLuong;
        private float thanhTien;

        public int MaHoaDon { get => maHoaDon; set => maHoaDon = value; }
        public int MaDoUong { get => maDoUong; set => maDoUong = value; }
        public DateTime? ThoiGianLap { get => thoiGianLap; set => thoiGianLap = value; }
        public DateTime? ThoiGianKet { get => thoiGianKet; set => thoiGianKet = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public float ThanhTien { get => thanhTien; set => thanhTien = value; }

        public BillInfoDTO(int maHoaDon, int maDoUong, DateTime? thoiGianLap, DateTime? thoiGianKet, int soLuong, float thanhTien)
        {
            this.MaHoaDon = maHoaDon;
            this.MaDoUong = maDoUong;
            this.ThoiGianLap = thoiGianLap;
            this.ThoiGianKet = thoiGianKet;
            this.SoLuong = soLuong;
            this.ThanhTien = ThanhTien;
        }

        public BillInfoDTO(DataRow row)
        {
            this.MaHoaDon = (int)row["mahoadon"];
            this.MaDoUong = (int)row["madouong"];
            this.ThoiGianLap = (DateTime?)row["thoigianlap"];
            var datetemp = row["datetemp"];
            if (datetemp.ToString() != "")
                this.ThoiGianKet = (DateTime?)datetemp;
            this.SoLuong = (int)row["soluong"];
            this.ThanhTien = (float)row["thanhtien"];
        }
    }
}
