using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiCoffe.DTO
{
    public class MaterialDTO
    {
        private int maNguyenLieu;
        private string tenNguyenLieu;
        private int maNhaCungCap;
        private float donGia;
        private string donVi;
        private int soLuongTon;
        private string ghiChu;

        public int MaNguyenLieu { get => maNguyenLieu; set => maNguyenLieu = value; }
        public string TenNguyenLieu { get => tenNguyenLieu; set => tenNguyenLieu = value; }
        public int MaNhaCungCap { get => maNhaCungCap; set => maNhaCungCap = value; }
        public float DonGia { get => donGia; set => donGia = value; }
        public int SoLuongTon { get => soLuongTon; set => soLuongTon = value; }
        public string GhiChu { get => ghiChu; set => ghiChu = value; }
        public string DonVi { get => donVi; set => donVi = value; }

        public MaterialDTO(int maNguyenLieu, string tenNguyenLieu, int maNhaCungCap, float donGia, string donVi, int soLuongTon, string ghiChu)
        {
            this.MaNguyenLieu = maNguyenLieu;
            this.TenNguyenLieu = tenNguyenLieu;
            this.MaNhaCungCap = maNhaCungCap;
            this.DonGia = donGia;
            this.DonVi = donVi;
            this.SoLuongTon = soLuongTon;
            this.GhiChu = ghiChu;
        }

        public MaterialDTO(DataRow row)
        {
            this.MaNguyenLieu = (int)row["manguyenlieu"];
            this.TenNguyenLieu = row["tennguyenlieu"].ToString();
            this.MaNhaCungCap = (int)row["manhacungcap"];
            this.DonGia = (float)row["dongia"];
            this.DonVi = row["donvi"].ToString();
            this.SoLuongTon = (int)row["soluongton"];
            this.GhiChu = row["ghichu"].ToString();
        }
    }
}
