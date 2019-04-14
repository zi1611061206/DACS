using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZiCoffe.DTO;

namespace ZiCoffe.DAO
{
    public class MenuDAO
    {
        #region Package_MenuDAO
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }
        }

        private MenuDAO() { }
        #endregion

        public List<MenuDTO> GetDrinkList(int maDanhMuc)
        {
            List<MenuDTO> drinksList = new List<MenuDTO>();
            string query = "select * from thucdon where madanhmuc= @madanhmuc ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maDanhMuc });

            foreach (DataRow item in data.Rows)
            {
                MenuDTO dataLine = new MenuDTO(item);
                drinksList.Add(dataLine);
            }
            return drinksList;
        }

        public DataTable GetMenu()
        {
            string query = "exec LoadListMenu";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool DeleteDrinks(int maDoUong)
        {
            BillInfoDAO.Instance.DeleteBillInfo2(maDoUong);
            string query = "delete dbo.thucdon where madouong= @madouong";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maDoUong });
            return result > 0;
        }

        public DataTable SearchMenu(string tenDoUong)
        {
            string query = string.Format("exec SearchMenu N'{0}'", tenDoUong);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool AddDrinks(string tenDoUong, int maDanhMuc, float giaBan, int tinhTrang, byte[] hinhAnh)
        {
            string query = "insert into dbo.thucdon (madanhmuc,tendouong,giaban,tinhtrang,hinhanh) values ( @madanhmuc , @tendouong , @giaban , @tinhtrang , @hinhanh )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maDanhMuc, tenDoUong, giaBan, tinhTrang, hinhAnh });
            return result > 0;
        }

        public bool ChangeDrinks(string tenDoUong, int maDanhMuc, float giaBan, int tinhTrang, byte[] hinhAnh, int maDoUong)
        {
            string query = "update dbo.thucdon set tendouong= @tendouong , madanhmuc= @madanhmuc ,  giaban=  @giaban , tinhtrang= @tinhtrang , hinhanh= @hinhanh where madouong= @madouong";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenDoUong, maDanhMuc, giaBan, tinhTrang, hinhAnh, maDoUong });
            return result > 0;
        }

        public bool ChangeDrinks2(string tenDoUong, int maDanhMuc, float giaBan, int tinhTrang, int maDoUong)
        {
            string query = "update dbo.thucdon set tendouong= @tendouong , madanhmuc= @madanhmuc ,  giaban=  @giaban , tinhtrang= @tinhtrang where madouong= @madouong";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenDoUong, maDanhMuc, giaBan, tinhTrang, maDoUong });
            return result > 0;
        }
    }
}
