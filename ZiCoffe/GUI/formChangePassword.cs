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
    public partial class formChangePassword : Form
    {
        private EmployeesDTO currentAccount;

        public EmployeesDTO CurrentAccount
        {
            get { return currentAccount; }
            set { currentAccount = value; }
        }

        public formChangePassword(EmployeesDTO account)
        {
            InitializeComponent();
            this.CurrentAccount = account;
        }

        #region Event
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txbNewPass.Text) || !txbNewPass.Text.Equals(txbReEnter.Text) )
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp hoặc thông tin yêu cầu bị bỏ trống. \n\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (LoginDAO.Instance.UpdatePassword(currentAccount.TenDangNhap, txbOldPass.Text, txbNewPass.Text))
                {
                    MessageBox.Show("Cập nhật mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Mật khẩu hiện tại của bạn không đúng. \n\nVui lòng điền đúng mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
