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
    public partial class formProfile : Form
    {
        private EmployeesDTO currentAccount;

        public EmployeesDTO CurrentAccount
        {
            get { return currentAccount; }
            set { currentAccount = value; ChangeAccount(currentAccount); }
        }

        public formProfile(EmployeesDTO account)
        {
            InitializeComponent();
            this.CurrentAccount = account;
        }

        #region Method
        void ChangeAccount(EmployeesDTO account)
        {
            txbUsername.Text = CurrentAccount.TenDangNhap;
            txbName.Text = CurrentAccount.HoTen;
            txbPosition.Text = CurrentAccount.MaChucVu.ToString();
            txbID.Text = CurrentAccount.ChungMinhNhanDan;
            txbAddress.Text = CurrentAccount.DiaChi;
            txbPhone.Text = CurrentAccount.SoDienThoai;
        }
        #endregion

        #region event
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            formChangePassword f = new formChangePassword(currentAccount);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
        #endregion


    }
}
