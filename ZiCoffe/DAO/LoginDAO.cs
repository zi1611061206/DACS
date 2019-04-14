using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZiCoffe.DTO;

namespace ZiCoffe.DAO
{
    public class LoginDAO
    {
        #region package_LoginDAO
        private static LoginDAO instance;
        public static LoginDAO Instance
        {
            get { if (instance == null) instance = new LoginDAO(); return instance; }
            private set { instance = value; }
        }
        private LoginDAO() { }
        #endregion

        public bool Login(string username, string password)
        {
            string query = "exec checklogin @username , @password";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });
            return data.Rows.Count > 0;
        }

        public EmployeesDTO GetEmployeesByUsername(string tenDangNhap)
        {
            string query = "select * from dbo.nhanvien where tendangnhap = @tendangnhap";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenDangNhap });

            foreach (DataRow item in data.Rows)
            {
                return new EmployeesDTO(item);
            }
            return null;
        }

        public bool UpdatePassword(string tenDangNhap, string matKhau, string matKhauMoi)
        {
            string query = "exec UpdatePassword @tendangnhap , @matkhau , @matkhaumoi ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenDangNhap, matKhau, matKhauMoi });
            return result > 0;
        }

        public bool DeleteAccount(string tenDangNhap)
        {
            string query = "delete dbo.taikhoan where tendangnhap= @tendangnhap";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenDangNhap });
            return result > 0;
        }

        public bool ResetPassword(string tenDangNhap)
        {
            string query = "update dbo.taikhoan set matkhau = 1 where tendangnhap = @tendangnhap";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenDangNhap });
            return result > 0;
        }

        public bool CheckExisted(string tenDangNhap)
        {
            string query = "select * from dbo.taikhoan where tendangnhap = @tendangnhap ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tenDangNhap });
            return data.Rows.Count > 0;
        }

        public bool AddAccount(string tenDangNhap)
        {
            string query = "insert into dbo.taikhoan (tendangnhap) values ( @tendangnhap )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenDangNhap });
            return result > 0;
        }
    }
}
