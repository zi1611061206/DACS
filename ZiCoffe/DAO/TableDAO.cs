using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZiCoffe.DTO;

namespace ZiCoffe.DAO
{
    public class TableDAO
    {
        #region Package_TableDAO
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }

        public static int tableWidth = 137; //Chiều dài
        public static int tableHeigh = 137; //Chiều rộng

        private TableDAO() { }
        #endregion

        public List<TableDTO> GetTableList()
        {
            List<TableDTO> tablelist = new List<TableDTO>();
            DataTable data = DataProvider.Instance.ExecuteQuery("exec GetTableList");
            foreach (DataRow item in data.Rows)
            {
                TableDTO table = new TableDTO(item);
                tablelist.Add(table);
            }
            return tablelist;
        }

        public List<TableDTO> GetTempTableList()
        {
            List<TableDTO> tempTableList = new List<TableDTO>();
            string query = "select * from dbo.ban where trangthai = 0";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                TableDTO dataLine = new TableDTO(item);
                tempTableList.Add(dataLine);
            }
            return tempTableList;
        }

        public List<TableDTO> GetFullTableList()
        {
            List<TableDTO> fullTableList = new List<TableDTO>();
            string query = "select * from dbo.ban where trangthai = 1";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                TableDTO dataLine = new TableDTO(item);
                fullTableList.Add(dataLine);
            }
            return fullTableList;
        }

        public int CountTableTotal()
        {
            string query = "select count(*) from dbo.ban";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public int CountFullTable()
        {
            string query = "select count(*) from dbo.ban where trangthai = 1";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public bool Insert_table(string tenBan)
        {
            string query1 = string.Format("insert into dbo.ban (tenban) values ( N'{0}' )", tenBan);
            DataProvider.Instance.ExecuteNonQuery(query1);

            string query2 = string.Format("select maban from dbo.ban where tenban = N'{0}' ", tenBan);
            int maBan = (int)DataProvider.Instance.ExecuteScalar(query2);

            string query3 = string.Format("update dbo.ban set tenban = N'Bàn '+'{0}' where maban = {1}", maBan, maBan);
            int result = DataProvider.Instance.ExecuteNonQuery(query3);

            return result > 0;
        }

        public bool DeleteTable(int maBan)
        {
            string query = "delete dbo.ban where maban = @maban ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { maBan });
            return result > 0;
        }
    }
}
