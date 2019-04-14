using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZiCoffe.DTO;

namespace ZiCoffe.DAO
{
    public class SupplierDAO
    {
        #region package_SupplierDAO
        private static SupplierDAO instance;
        public static SupplierDAO Instance
        {
            get { if (instance == null) instance = new SupplierDAO(); return instance; }
            private set { instance = value; }
        }
        private SupplierDAO() { }
        #endregion

        public List<SupplierDTO> GetSupplierList()
        {
            List<SupplierDTO> supplierList = new List<SupplierDTO>();
            string query = "select * from nhacungcap";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                SupplierDTO dataLine = new SupplierDTO(item);
                supplierList.Add(dataLine);
            }
            return supplierList;
        }

        public DataTable GetSupplier()
        {
            string query = "exec LoadListSupplier";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable SearchSupplier(string tenNhaCungCap)
        {
            string query = string.Format("exec SearchSupplier N'{0}'", tenNhaCungCap);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool AddSupplier(string tenNhaCungCap, string diaChi, string soDienThoai, string email, string ghiChu)
        {
            string query = "insert into dbo.nhacungcap (tennhacungcap, diachi, sodienthoai, email, ghichu) values ( @tennhacungcap , @diachi , @sodienthoai , @email , @ghichu )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenNhaCungCap, diaChi, soDienThoai, email, ghiChu });
            return result > 0;
        }

        public bool DeleteSupplier(int maNhaCungCap)
        {
            List<MaterialDTO> materialList = MaterialInfoDAO.Instance.GetMaterialList(maNhaCungCap);
            foreach (MaterialDTO item in materialList)
            {
                MaterialInfoDAO.Instance.DeleteMaterialInfo2(maNhaCungCap);
                MaterialDAO.Instance.DeleteMaterial(item.MaNguyenLieu);
            }

            string query = "delete dbo.nhacungcap where manhacungcap= @manhacungcap ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNhaCungCap });
            return result > 0;
        }

        public bool ChangeSupplier(string tenNhaCungCap, string diaChi, string soDienThoai, string email, string ghiChu, int maNhaCungCap)
        {
            string query = "update dbo.nhacungcap set tennhacungcap= @tennhacungcap , diachi= @diachi , sodienthoai= @sodienthoai , email= @email , ghichu= @ghichu where manhacungcap= @manhacungcap ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenNhaCungCap, diaChi, soDienThoai, email, ghiChu, maNhaCungCap });
            return result > 0;
        }
    }
}
