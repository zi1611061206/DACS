using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiCoffe.DAO
{
    public class BillInfoDAO
    {
        #region Package_Billinfo_DAO
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set { BillInfoDAO.instance = value; }
        }

        private BillInfoDAO() { }
        #endregion

        public bool UpdateBillInfo(int maHoaDon)
        {
            string query = "update dbo.chitiethoadon set thoigianket = getdate() where mahoadon = @mahoadon";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maHoaDon });
            return result > 0;
        }

        public void InsertBillInfo(int maHoaDon, int maDoUong, int soLuong)
        {
            string query = "exec UpdateIntoExistedBill @mahoadon , @madouong , @soluong";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { maHoaDon, maDoUong, soLuong});
        }

        public bool DeleteBillInfo(int maHoaDon)
        {
            return DataProvider.Instance.ExecuteNonQuery("delete dbo.chitiethoadon where mahoadon= " + maHoaDon) > 0;
        }

        public bool DeleteBillInfo2(int maDoUong)
        {
            return DataProvider.Instance.ExecuteNonQuery("delete dbo.chitiethoadon where madouong= " + maDoUong) > 0;
        }
    }
}
