using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZiCoffe.DAO
{
    public class DataProvider
    {
        private string connectionString = @"Data Source=ADMIN-PC\SQLEXPRESS;Initial Catalog=QLCF;Integrated Security=True";

        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        //Các nhóm truy vấn dữ liệu
        #region Trả dữ liệu về dưới dạng bảng (dùng khi truy vấn thông thường, cần xem dữ liệu)
        public DataTable ExecuteQuery(string query, object[] parameterArray = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameterArray != null)
                {
                    string[] parameterList = query.Split(' ');
                    int i = 0;
                    foreach (string item in parameterList)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameterArray[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }
        #endregion

        #region Trả về số dòng thành công (dùng khi INSERT-DELETE-UPDATE, cần biết đã thực hiện thành công hay chưa)
        public int ExecuteNonQuery(string query, object[] parameterArray = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameterArray != null)
                {
                    string[] parameterList = query.Split(' ');
                    int i = 0;
                    foreach (string item in parameterList)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameterArray[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }
        #endregion

        #region Trả dữ liệu về dưới dạng một object (dùng khi cần lấy một dữ liệu có kiểu bất kỳ sau đó ép kiểu theo yêu cầu)
        public object ExecuteScalar(string query, object[] parameterArray = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameterArray != null)
                {
                    string[] parameterList = query.Split(' ');
                    int i = 0;
                    foreach (string item in parameterList)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameterArray[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteScalar();
                connection.Close();
            }
            return data;
        }
        #endregion
    }
}
