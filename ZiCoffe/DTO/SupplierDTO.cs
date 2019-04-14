using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiCoffe.DTO
{
    public class SupplierDTO
    {
        private int maNhaCungCap;
        private string tenNhaCungCap;
        private string diaChi;
        private string soDienThoai;
        private string email;
        private string ghiChu;

        public int MaNhaCungCap { get => maNhaCungCap; set => maNhaCungCap = value; }
        public string TenNhaCungCap { get => tenNhaCungCap; set => tenNhaCungCap = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }
        public string Email { get => email; set => email = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }

        public SupplierDTO(int maNhaCungCap, string tenNhaCungCap, string diaChi, string soDienThoai, string email, string ghiChu)
        {
            this.MaNhaCungCap = maNhaCungCap;
            this.TenNhaCungCap = tenNhaCungCap;
            this.DiaChi = diaChi;
            this.SoDienThoai = tenNhaCungCap;
            this.Email = email;
            this.GhiChu = ghiChu;
        }

        public SupplierDTO(DataRow row)
        {
            this.MaNhaCungCap = (int)row["manhacungcap"];
            this.TenNhaCungCap = row["tennhacungcap"].ToString();
            this.DiaChi = row["diachi"].ToString();
            this.SoDienThoai = row["sodienthoai"].ToString();
            this.Email = row["email"].ToString();
            this.GhiChu = row["ghichu"].ToString();
        }
    }
}
