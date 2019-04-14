using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZiCoffe.DTO;

namespace ZiCoffe.DAO
{
    public class MaterialInfoDAO
    {
        #region package_MaterialInfoDAO
        private static MaterialInfoDAO instance;

        public static MaterialInfoDAO Instance
        {
            get { if (instance == null) instance = new MaterialInfoDAO(); return MaterialInfoDAO.instance; }
            private set { MaterialInfoDAO.instance = value; }
        }

        private MaterialInfoDAO() { }
        #endregion

        public List<MaterialDTO> GetMaterialList(int maNhaCungCap)
        {
            List<MaterialDTO> materialList = new List<MaterialDTO>();
            string query = "select * from chitietnguyenlieu where manhacungcap= @manhacungcap ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maNhaCungCap });

            foreach (DataRow item in data.Rows)
            {
                MaterialDTO dataLine = new MaterialDTO(item);
                materialList.Add(dataLine);
            }
            return materialList;
        }

        public bool AddMaterialInfo(int maNguyenLieu, string tenNguyenLieu, int maNhaCungCap, float donGia, string donVi, int soLuong, string ghiChu)
        {
            string query = "insert into dbo.chitietnguyenlieu (manguyenlieu,tennguyenlieu,manhacungcap,dongia,donvi,soluongton,ghichu) values ( @manguyenlieu , @tennguyenlieu , @manhacungcap , @dongia , @donvi , @soluong , @ghichu )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNguyenLieu, tenNguyenLieu, maNhaCungCap, donGia, donVi, soLuong, ghiChu });
            return result > 0;
        }

        public bool DeleteMaterialInfo(int maNguyenLieu)
        {
            string query = "delete dbo.chitietnguyenlieu where manguyenlieu= @manguyenlieu ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNguyenLieu });
            return result > 0;
        }

        public bool DeleteMaterialInfo2(int maNhaCungCap)
        {
            string query = "delete dbo.chitietnguyenlieu where manhacungcap= @manhacungcap ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNhaCungCap });
            return result > 0;
        }

        public bool ChangeMaterialInfo(int maNguyenLieu, string tenNguyenLieu, int maNhaCungCap, float donGia, string donVi, int soLuong, string ghiChu)
        {
            string query = "update dbo.chitietnguyenlieu set tennguyenlieu= @tennguyenlieu , manhacungcap= @manhachungcap , dongia= @dongia , donvi= @donvi , soluongton= @soluong , ghichu= @ghichu where manguyenlieu= @manguyenlieu ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tenNguyenLieu, maNhaCungCap, donGia, donVi, soLuong, ghiChu, maNguyenLieu });
            return result > 0;
        }
    }
}
