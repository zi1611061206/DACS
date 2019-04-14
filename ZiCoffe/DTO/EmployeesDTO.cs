using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiCoffe.DTO
{
    public class EmployeesDTO
    {
        private int maNhanVien;
        private string tenDangNhap;
        private string hoTen;
        private int gioiTinh;
        private int maChucVu;
        private string chungMinhNhanDan;
        private string diaChi;
        private string soDienThoai;

        public int MaNhanVien
        {
            get { return maNhanVien; }
            set { maNhanVien = value; }
        }
        public string TenDangNhap
        {
            get { return tenDangNhap; }
            set { tenDangNhap = value; }
        }
        public string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }
        public int GioiTinh
        {
            get { return gioiTinh; }
            set { gioiTinh = value; }
        }
        public int MaChucVu
        {
            get { return maChucVu; }
            set { maChucVu = value; }
        }
        public string ChungMinhNhanDan
        {
            get { return chungMinhNhanDan; }
            set { chungMinhNhanDan = value; }
        }
        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }
        public string SoDienThoai
        {
            get { return soDienThoai; }
            set { soDienThoai = value; }
        }
        
        public EmployeesDTO(int maNhanVien, string tenDangNhap, string hoTen,int gioiTinh, int maChucVu, string chungMinhNhanDan, string diaChi, string soDienThoai)
        {
            this.MaNhanVien = maNhanVien;
            this.TenDangNhap = tenDangNhap;
            this.HoTen = hoTen;
            this.GioiTinh = gioiTinh;
            this.MaChucVu = maChucVu;
            this.ChungMinhNhanDan = chungMinhNhanDan;
            this.DiaChi = diaChi;
            this.SoDienThoai = soDienThoai;
        }

        public EmployeesDTO(DataRow row)
        {
            this.MaNhanVien = (int)row["manhanvien"];
            this.TenDangNhap = row["tendangnhap"].ToString();
            this.MaChucVu = (int)row["machucvu"];
            this.HoTen = row["hoten"].ToString();
            this.GioiTinh = Convert.ToInt32(row["gioitinh"]);
            this.ChungMinhNhanDan = row["chungminhnhandan"].ToString();
            this.DiaChi = row["diachi"].ToString();
            this.SoDienThoai = row["sodienthoai"].ToString();
        }
    }
}
