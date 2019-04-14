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
    public partial class formNewEmloyees : Form
    {
        public formNewEmloyees()
        {
            InitializeComponent();
            LoadPosition(cbPosition);
        }

        #region Method
        void LoadPosition(ComboBox cb)
        {
            cb.DataSource = PositionDAO.Instance.GetPositionList();
            cb.DisplayMember = "tenchucvu";
        }
        #endregion

        #region Event
        private void btnAgree_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;
            string name = txbName.Text;
            int positionID = (cbPosition.SelectedItem as PositionDTO).MaChucVu;
            string idNum = txbIDNum.Text;
            string address = txbAddress.Text;
            string phone = txbPhone.Text;

            if (LoginDAO.Instance.CheckExisted(username))
            {
                MessageBox.Show("Tài khoản " + username + " đã tồn tại\nVui lòng chọn tên đăng nhập khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (LoginDAO.Instance.AddAccount(username))
                {
                    if (EmployeesDAO.Instance.AddEmployees(username, name, positionID, idNum, address, phone))
                    {
                        MessageBox.Show("Thêm thành công!\n\n- Tên đăng nhập: " + username + "\n- Mật khẩu mặc định của bạn là: 1\n\nBạn có thể đăng nhập để đổi lại mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDisagree_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
