using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiCoffe.DTO
{
    public class MenuDTO
    {
        private int maDoUong;
        private int maDanhMuc;
        private string tenDoUong;
        private float giaBan;
        private int tinhTrang;

        public int MaDoUong { get => maDoUong; set => maDoUong = value; }
        public int MaDanhMuc { get => maDanhMuc; set => maDanhMuc = value; }
        public string TenDoUong { get => tenDoUong; set => tenDoUong = value; }
        public float GiaBan { get => giaBan; set => giaBan = value; }
        public int TinhTrang { get => tinhTrang; set => tinhTrang = value; }

        public MenuDTO(int maDoUong, int maDanhMuc, string tenDoUong, float giaBan, int tinhTrang)
        {
            this.MaDoUong = maDoUong;
            this.MaDanhMuc = maDanhMuc;
            this.TenDoUong = tenDoUong;
            this.GiaBan = giaBan;
            this.TinhTrang = tinhTrang;
        }

        public MenuDTO(DataRow row)
        {
            this.MaDoUong = (int)row["madouong"];
            this.MaDanhMuc = (int)row["madanhmuc"];
            this.TenDoUong = row["tendouong"].ToString();
            this.GiaBan = (float)Convert.ToDouble(row["giaban"].ToString());
            this.TinhTrang = Convert.ToInt32(row["tinhtrang"]);
        }

    }
}
