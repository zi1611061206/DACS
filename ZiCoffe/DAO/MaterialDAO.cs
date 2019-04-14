using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZiCoffe.DTO;

namespace ZiCoffe.DAO
{
    public class MaterialDAO
    {
        #region package_MaterialDAO
        private static MaterialDAO instance;

        public static MaterialDAO Instance
        {
            get { if (instance == null) instance = new MaterialDAO(); return MaterialDAO.instance; }
            private set { MaterialDAO.instance = value; }
        }

        private MaterialDAO() { }
        #endregion

        public DataTable GetMaterial()
        {
            string query = "exec LoadListMaterial";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable SearchMaterial(string tenNguyenLieu)
        {
            string query = string.Format("exec SearchMaterial N'{0}'", tenNguyenLieu);
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool AddMaterial(int tingTrang)
        {
            string query = "insert into dbo.nguyenlieu (tinhtrang) values ( @tinhtrang )";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tingTrang });
            return result > 0;
        }

        public int GetNewMaterialID()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("select max(manguyenlieu) from nguyenlieu");
            }
            catch
            {
                return 1;
            }
        }

        public bool DeleteMaterial(int maNguyenLieu)
        {
            string query = "delete dbo.nguyenlieu where manguyenlieu= @manguyenlieu";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maNguyenLieu });
            return result > 0;
        }

        public bool ChangeMaterial(int tinhTrang, int maNguyenLieu)
        {
            string query = "update dbo.nguyenlieu set tinhtrang= @tinhtrang where manguyenlieu= @manguyenlieu ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tinhTrang, maNguyenLieu });
            return result > 0;
        }
    }
}
