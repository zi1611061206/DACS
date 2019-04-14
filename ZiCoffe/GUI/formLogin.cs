using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZiCoffe.DAO;
using ZiCoffe.DTO;

namespace ZiCoffe.GUI
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        #region Hàm xử lí thoát
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void formLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region Hàm xử lí đăng nhập
        bool Login(string username, string password)
        {
            return LoginDAO.Instance.Login(username, password);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text.ToString();
            string password = txbPassword.Text.ToString();

            if (username != "" || password != "")
            {
                if (Login(username, password))
                {
                    AllowAccess(username);
                }
                else
                {
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu","Cảnh báo", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void AllowAccess(string username)
        {
            EmployeesDTO accountName = LoginDAO.Instance.GetEmployeesByUsername(username);
            formMain f = new formMain(accountName);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
        #endregion

    }
}
