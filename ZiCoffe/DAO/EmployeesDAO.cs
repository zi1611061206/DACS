using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZiCoffe.DTO;

namespace ZiCoffe.DAO
{
    public class EmployeesDAO
    {
        #region package_EmployeesDAO
        private static EmployeesDAO instance;

        public static EmployeesDAO Instance
        {
            get { if (instance == null) instance = new EmployeesDAO(); return EmployeesDAO.instance; }
            private set { EmployeesDAO.instance = value; }
        }

        private EmployeesDAO() { }
        #endregion

        public DataTable GetEmployees()
        {
            string query = "exec LoadListEmployees";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public List<EmployeesDTO> GetEmployeesList(int maChucVu)
        {
            List<EmployeesDTO> employeesList = new List<EmployeesDTO>();
            string query = "select * from nhanvien where machucvu= @machucvu ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maChucVu });

            foreach (DataRow item in data.Rows)
            {
                EmployeesDTO dataLine = new EmployeesDTO(item);
                employeesList.Add(dataLine);
            }
            return employeesList;
        }

        public DataTable SearchEmployees(string hoTen)
        {
            string query = string.Format("exec SearchEmployees N'{0}'", hoTen);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool DeleteEmployees(int maChucVu)
        {
            string query = "delete dbo.nhanvien where machucvu= @machucvu";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maChucVu });
            return result > 0;
        }

        public bool DeleteEmployees2(string tenDangNhap)
        {
            string query = "delete dbo.nhanvien where tendangnhap= @tendangnhap";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenDangNhap });
            return result > 0;
        }

        public bool ChangeEmployees(string hoTen, string chungMinhNhanDan, string diaChi, string soDienThoai, int gioiTinh, int maChucVu, byte[] anhDaiDien, string tenDangNhap)
        {
            string query = "update dbo.nhanvien set hoten = @hoten , chungminhnhandan = @chungminhnhandan , diachi = @diachi , sodienthoai = @sodienthoai , gioitinh= @gioitinh , machucvu= @machucvu , anhdaidien= @anhdaidien where tendangnhap = @tendangnhap ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { hoTen, chungMinhNhanDan, diaChi, soDienThoai, gioiTinh, maChucVu, anhDaiDien, tenDangNhap });
            return result > 0;
        }

        public bool ChangeEmployees2(string hoTen, string chungMinhNhanDan, string diaChi, string soDienThoai, int gioiTinh, int maChucVu, string tenDangNhap)
        {
            string query = "update dbo.nhanvien set hoten = @hoten , chungminhnhandan = @chungminhnhandan , diachi = @diachi , sodienthoai = @sodienthoai , gioitinh= @gioitinh , machucvu= @machucvu  where tendangnhap = @tendangnhap ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { hoTen, chungMinhNhanDan, diaChi, soDienThoai, gioiTinh, maChucVu, tenDangNhap });
            return result > 0;
        }

        public bool AddEmployees(string tenDangNhap, string hoTen, int maChucVu, string chungMinhNhanDan, string diaChi, string soDienthoai)
        {
            string query = "insert into dbo.nhanvien (tendangnhap,hoten,machucvu,chungminhnhandan,diachi,sodienthoai) values ( @tendangnhap , @hoten , @machucvu , @chungminhnhandan , @diachi , @sodienthoai )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenDangNhap, hoTen, maChucVu, chungMinhNhanDan, diaChi, soDienthoai });
            return result > 0;
        }
    }
}
