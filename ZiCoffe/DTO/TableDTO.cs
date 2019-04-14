using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiCoffe.DTO
{
    public class TableDTO
    {
        private int maBan;
        private string tenBan;
        private int trangThai;

        public int MaBan { get => maBan; set => maBan = value; }
        public string TenBan { get => tenBan; set => tenBan = value; }
        public int TrangThai { get => trangThai; set => trangThai = value; } 

        public TableDTO(int maBan, string tenBan, int trangThai)
        {
            this.MaBan = maBan;
            this.TenBan = tenBan;
            this.TrangThai = trangThai;
        }

        public TableDTO(DataRow row)
        {
            this.MaBan = (int)row["maban"];
            this.TenBan = row["tenban"].ToString();
            this.TrangThai = Convert.ToInt32(row["trangthai"]);
        }
    }
}
