using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZiCoffe.DTO;

namespace ZiCoffe.DAO
{
    public class BillDAO
    {
        #region Package_BillDAO
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }

        private BillDAO() { }
        #endregion

        public int GetBillID(int maBan)
        {
            string query = "select * from dbo.hoadon where maban= @maban and trangthai = 0 ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maBan });
            if (data.Rows.Count > 0)
            {
                BillDTO bill = new BillDTO(data.Rows[0]);
                return bill.MaHoaDon;
            }
            return -1;
        }

        public void InsertBill(int maBan)
        {
            string query = "insert into dbo.hoadon (maban) values ( @maban )";
            DataProvider.Instance.ExecuteQuery(query, new object[] { maBan });
        }

        public bool UpdateBill(double giamGia, double tongTien, int maBan)
        {
            string query = "update dbo.hoadon set giamgia= @giamgia ,tongtien= @tongtien , trangthai = 1 where maban= @maban and trangthai = 0";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { giamGia, tongTien, maBan });
            return result > 0;
        }

        public int GetMaxBillID()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("select max(mahoadon) from hoadon");
            }
            catch
            {
                return 1;
            }
        }

        public bool UpdateMoveTable(int maBan, int maHoaDon)
        {
            string query = "update dbo.hoadon set maban = @maban where mahoadon= @mahoadon";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maBan, maHoaDon });
            return result > 0;
        }

        public bool UpdateGatherTable(int maBanDich, int maHoaDonNguon)
        {
            string query = "update dbo.hoadon set maban = @maBandich , ghichu = N'101: gộp bàn' where mahoadon = @mahoadonnguon";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maBanDich, maHoaDonNguon });
            return result > 0;
        }

        public bool UpdateTableStatus(int maBanNguon)
        {
            string query = "update dbo.ban set trangthai = N'Trống' where maban = @mabannguon";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maBanNguon });
            return result > 0;
        }

        public List<BillDTO> GetListBillID(int maBan)
        {
            List<BillDTO> billIDList = new List<BillDTO>();
            string query = "select * from dbo.hoadon where maban= @maban ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maBan });
            foreach(DataRow item in data.Rows)
            {
                BillDTO dataLine = new BillDTO(item);
                billIDList.Add(dataLine);
            }
            return billIDList;
        }

        public bool DeleteBill(int maHoaDon)
        {
            string query = "delete dbo.hoadon where mahoadon = @mahoadon";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maHoaDon });
            return result > 0;
        }

        public DataTable GetRevenue(DateTime thoiGianDau, DateTime thoiGianCuoi, int soTrang, int soDongTrenTrang)
        {
            string query = "exec GetRevenue @thoigiandau , @thoigiancuoi , @pagenumber , @max_number_rows_ofpage";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { thoiGianDau, thoiGianCuoi, soTrang, soDongTrenTrang });
        }

        public int GetDisPlayRecord(DateTime thoiGianDau, DateTime thoiGianCuoi, int soTrang, int soDongTrenTrang)
        {
            string query = "exec GetDisplayRecord @thoigiandau , @thoigiancuoi , @pagenumber , @max_number_rows_ofpage";
            int display = (int)DataProvider.Instance.ExecuteScalar(query, new object[] { thoiGianDau, thoiGianCuoi, soTrang, soDongTrenTrang });
            return display;
        }

        // Revenue

        public int GetRevenueRecordNum(DateTime thoiGianDau, DateTime thoiGianCuoi)
        {
            string query = "exec GetRevenueRecordNum @thoigianlap , @thoigianthanhtoan ";
            return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { thoiGianDau, thoiGianCuoi });
        }
    }
}
