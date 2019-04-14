using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZiCoffe.DAO;
using ZiCoffe.DTO;

namespace ZiCoffe.GUI
{
    public partial class formMain : Form
    {
        private EmployeesDTO currentAccount;

        public EmployeesDTO CurrentAccount
        {
            get { return currentAccount; }
            set { currentAccount = value; ChangeAccount(currentAccount.MaChucVu); }
        }

        public formMain(EmployeesDTO accountName)
        {
            InitializeComponent();
            pnlPrint.Hide();
            this.CurrentAccount = accountName;
            ReLoad("12345");
            btnGatherTable.Enabled = false;
        }
        
        #region Method
        #region [Other]
        private void ChangeAccount(int positionID)
        {
            quảnLýToolStripMenuItem.Enabled = positionID == 1;
            btnAddTable.Enabled = positionID == 1;
            btnDeleteTable.Enabled = positionID == 1;
            xemThôngTinToolStripMenuItem.Text += " (" + CurrentAccount.TenDangNhap + ")";
        }

        private void ConfigureTable(TableDTO item)
        {
            Button btnTable = new Button()
            {
                Width = TableDAO.tableWidth,
                Height = TableDAO.tableHeigh
            };
            btnTable.Text = item.TenBan + Environment.NewLine + (item.TrangThai == 0 ? "Trống" : "Có Người");
            switch (item.TrangThai)
            {
                case 0:
                    btnTable.BackColor = Color.SkyBlue;
                    break;
                default:
                    btnTable.BackColor = Color.Orange;
                    break;
            }
            btnTable.TabStop = false;
            btnTable.Tag = item;
            btnTable.Click += btnTable_Click;
            fpnlTableMap.Controls.Add(btnTable);
        }

        private void ShowBill(int tableID)
        {
            lstViewBill.Items.Clear();
            double total = 0f;
            List<ShowBillDTO> billInfoList = ShowBillDAO.Instance.GetBillByTableID(tableID);
            int countLine = 0;
            foreach (ShowBillDTO item in billInfoList)
            {
                ListViewItem listViewItem = new ListViewItem(item.TenDoUong.ToString());
                listViewItem.SubItems.Add(item.SoLuong.ToString());
                listViewItem.SubItems.Add(item.DonGia.ToString());
                listViewItem.SubItems.Add(item.ThanhTien.ToString());
                lstViewBill.Items.Add(listViewItem);
                total += (float)item.ThanhTien;
                countLine++;
                if (countLine % 2 == 0)
                    listViewItem.BackColor = Color.White;
                else
                    listViewItem.BackColor = Color.LightBlue;
            }
            double promotion = Convert.ToDouble(nudPromotion.Value);
            total = total * (100 - promotion) / 100;
            txbTotal.Text = total.ToString();
        }
        
        private bool IsSelectTable(TableDTO selectedTable)
        {
            if (selectedTable == null)
            {
                MessageBox.Show("Vui lòng chọn bàn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private bool IsSelectAmount(int amount)
        {
            if (amount == 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void Order(TableDTO selectedTable, int amount)
        {
            int billID = BillDAO.Instance.GetBillID(selectedTable.MaBan);
            int DrinksID = (cbDrink.SelectedItem as MenuDTO).MaDoUong;
            if (billID == -1)
            {
                BillDAO.Instance.InsertBill(selectedTable.MaBan);
                billID = BillDAO.Instance.GetMaxBillID();
                BillInfoDAO.Instance.InsertBillInfo(billID, DrinksID, amount);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(billID, DrinksID, amount);
            }
        }

        private void CalculateSurplus()
        {
            try
            {
                if (txbCash.Text != null)
                {
                    double surplus = Convert.ToDouble(txbCash.Text) - Convert.ToDouble(txbTotal.Text);
                    txbSurplus.Text = surplus.ToString();
                }
            }
            catch
            {
                txbCash.Text = "0";
            }
        }

        private void Document(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Thiết kế trang in 

            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 12, FontStyle.Regular);
            float fontheight = Font.GetHeight();
            int startX = 10;
            int startY = 10;
            int offset = 50;

            graphics.DrawString("ZI Coffee Shop", new Font("Courier New", 18, FontStyle.Bold), new SolidBrush(Color.Black), startX + 60, startY);
            graphics.DrawString("Nhân viên: " + currentAccount.TenDangNhap, font, new SolidBrush(Color.Black), startX + 105, startY + offset);
            offset += (int)fontheight;
            graphics.DrawString("Thời gian: " + DateTime.Now.ToString(), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset += (int)fontheight + 30;
            graphics.DrawString("Tên đồ uống", font, new SolidBrush(Color.Black), startX, startY + offset);
            graphics.DrawString("SL", font, new SolidBrush(Color.Black), startX + 225, startY + offset);
            graphics.DrawString("Giá", font, new SolidBrush(Color.Black), startX + 280, startY + offset);
            offset += (int)fontheight;
            graphics.DrawString("-----------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset += (int)fontheight;
            foreach (ListViewItem item in lstViewBill.Items)
            {
                graphics.DrawString(item.Text, font, new SolidBrush(Color.Black), startX, startY + offset);
                graphics.DrawString(item.SubItems[1].Text, font, new SolidBrush(Color.Black), startX + 225, startY + offset);
                graphics.DrawString(item.SubItems[3].Text, font, new SolidBrush(Color.Black), startX + 280, startY + offset);
                offset += (int)fontheight;
            }

            graphics.DrawString("-----------------------------------", font, new SolidBrush(Color.Black), startX, startY + offset);
            offset += (int)fontheight + 5;
            double total = Convert.ToDouble(txbTotal.Text) * 100 / (100 - Convert.ToDouble(nudPromotion.Value));
            graphics.DrawString("Tổng cộng (VNĐ): ".PadRight(27) + total.ToString(), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset += (int)fontheight + 5;
            graphics.DrawString("Khuyến mại (%): ".PadRight(27) + nudPromotion.Value.ToString(), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset += (int)fontheight + 5;
            graphics.DrawString("Thành tiền (VNĐ): ".PadRight(27) + txbTotal.Text.ToString(), new Font("Courier New", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            offset += (int)fontheight + 5;
            graphics.DrawString("Tiền nhận (VNĐ): ".PadRight(27) + txbCash.Text.ToString(), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset += (int)fontheight + 5;
            graphics.DrawString("Còn dư (VNĐ): ".PadRight(27) + txbSurplus.Text.ToString(), font, new SolidBrush(Color.Black), startX, startY + offset);
            offset += (int)fontheight + 30;
            graphics.DrawString("Địa chỉ: 475A Điện Biên Phủ", font, new SolidBrush(Color.Black), startX + 20, startY + offset);
            offset += (int)fontheight;
            graphics.DrawString(" P25, Q.Bình Thạnh, TP HCM", font, new SolidBrush(Color.Black), startX + 20, startY + offset);
            offset += (int)fontheight;
            graphics.DrawString("ĐT: 0943 144 178", font, new SolidBrush(Color.Black), startX + 70, startY + offset);
            offset += (int)fontheight + 10;
            graphics.DrawString("Hân hạnh được phục vụ quý khách!", new Font("Courier New", 14, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + offset);
            offset += (int)fontheight + 5;
            graphics.DrawImage(Image.FromFile(Application.StartupPath + "\\Resources\\logoinmain.jpg"), startX + 130, startY + offset);
        }

        private void Pay()
        {
            TableDTO selectedTable = lstViewBill.Tag as TableDTO;
            int billID = BillDAO.Instance.GetBillID(selectedTable.MaBan);
            //string billIDWithNote = BillDAO.Instance.GetBillIDWithNote(selectedTable.MaBan).ToString();
            double promotion = Convert.ToDouble(nudPromotion.Value);
            double total = Convert.ToDouble(txbTotal.Text);

            if (BillDAO.Instance.UpdateBill(promotion, total, selectedTable.MaBan))
            {
                //if(billIDWithNote==null)
                //{
                //    BillInfoDAO.Instance.UpdateBillInfo(billID);
                //}
                //else
                //{
                    BillInfoDAO.Instance.UpdateBillInfo(billID);
                    //BillInfoDAO.Instance.UpdateBillInfo(int.Parse(billIDWithNote));
                //}
                ReLoad("1345");
                ShowBill(selectedTable.MaBan);
            }
        }

        private void ReLoad(string loadString)
        {
            switch (loadString)
            {
                case "1345":
                    LoadTable();
                    LoadTempTableList();
                    LoadFullTableList();
                    LoadFooter();
                    break;
                case "134":
                    LoadTable();
                    LoadTempTableList();
                    LoadFullTableList();
                    break;
                default:
                    LoadTable();
                    LoadCategoryList();
                    LoadTempTableList();
                    LoadFullTableList();
                    LoadFooter();
                    break;
            }
        }

        private void DeleteTable(TableDTO selectedTable)
        {
            if (TableDAO.Instance.DeleteTable(selectedTable.MaBan))
            {
                ReLoad("1345");
            }
        }

        private void MoveTable(TableDTO selectedTable)
        {
            int billID = BillDAO.Instance.GetBillID(selectedTable.MaBan);
            if (billID != -1)
            {
                int newTableID = (cbTempTable.SelectedItem as TableDTO).MaBan;
                if (BillDAO.Instance.UpdateMoveTable(newTableID, billID))
                {
                    ReLoad("134");
                    ShowBill(newTableID);
                    MessageBox.Show("Chuyển Bàn " + selectedTable.MaBan + " đến Bàn " + newTableID + " thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Chuyển Bàn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GatherTable(TableDTO selectedTable)
        {
            //Lấy mahoadon (NGUỒN)
            int billSourceID = BillDAO.Instance.GetBillID(selectedTable.MaBan);
            if (billSourceID != -1)
            {
                //Lấy maban (ĐÍCH)
                int tableDestinationID = (cbFullTable.SelectedItem as TableDTO).MaBan;
                //Cập nhật maban cho nguồn
                if (BillDAO.Instance.UpdateGatherTable(tableDestinationID, billSourceID))
                {
                    if (BillDAO.Instance.UpdateTableStatus(selectedTable.MaBan))
                    {
                        ReLoad("1345");
                        ShowBill(tableDestinationID);
                        MessageBox.Show("Gộp Bàn " + selectedTable.MaBan + " vào Bàn " + tableDestinationID + " thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Gộp Bàn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region [Load]
        private void LoadTable()
        {
            fpnlTableMap.Controls.Clear();
            List<TableDTO> tableList = TableDAO.Instance.GetTableList();
            foreach (TableDTO item in tableList)
            {
                ConfigureTable(item);
            }
        }

        private void LoadCategoryList()
        {
            List<CategoryDTO> categoryList = CategoryDAO.Instance.GetCategoryList();
            cbCategory.DataSource = categoryList;
            cbCategory.DisplayMember = "tendanhmuc";
        }

        private void LoadDrinksList(int maDanhMuc)
        {
            List<MenuDTO> drinksList = MenuDAO.Instance.GetDrinkList(maDanhMuc);
            cbDrink.DataSource = drinksList;
            cbDrink.DisplayMember = "tendouong";
        }

        private void LoadTempTableList()
        {
            List<TableDTO> tempTableList = TableDAO.Instance.GetTempTableList();
            cbTempTable.DataSource = tempTableList;
            cbTempTable.DisplayMember = "maban";
        }

        private void LoadFullTableList()
        {
            List<TableDTO> fullTableList = TableDAO.Instance.GetFullTableList();
            cbFullTable.DataSource = fullTableList; 
            cbFullTable.DisplayMember = "maban"; 
        }

        private void LoadFooter()
        {
            int allTable = TableDAO.Instance.CountTableTotal(); 
            lbCountTable.Text = "Tổng bàn: " + allTable;
            int fullTable = TableDAO.Instance.CountFullTable();
            lbCountFullTable.Text = "Đang sử dụng: " + fullTable;
            int tempTable = allTable - fullTable;
            lbCountTempTable.Text = "Trống: " + tempTable;
            double ratio = (double)fullTable / (double)allTable * 100;
            lbPercent.Text = string.Format("Tỷ lệ: {0} %", ratio);
        }
        #endregion
        #endregion

        #region Event
        #region [Controller]
        private void btnTable_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as TableDTO).MaBan;
            lstViewBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbCatagory = sender as ComboBox;
            if (cbCategory.SelectedItem == null)
                return;
            CategoryDTO selected = cbCategory.SelectedItem as CategoryDTO;
            int categoryID = selected.MaDanhMuc;
            LoadDrinksList(categoryID);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TableDTO selectedTable = lstViewBill.Tag as TableDTO;
            int amount = (int)nudAmount.Value;
            try
            {
                if (IsSelectTable(selectedTable) && IsSelectAmount(amount))
                {
                    Order(selectedTable, amount);
                    ReLoad("1345");
                    ShowBill(selectedTable.MaBan);
                }
                else
                {
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            //Mở công cụ calculator của window
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c start calc";
                process.StartInfo.UseShellExecute = false;
                process.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            /* Cách sử dụng cmd trong c#
            // khai bao lop thu vien
            using System.Diagnostics;
            
            // khoi tao tien trinh
            Process process = new Process();
            
            // thiet lap thong so tien trinh
            process.StartInfo.FileName = "cmd.exe"; // đường dẫn chương trình
            process.StartInfo.Arguments = "/k {duong dan file cmd/bat hoac lenh cmd}"; // tham so /c (thực hiện lệnh và tắt ctr chạy) /k (giữ lại ctr chạy)
            process.StartInfo.UseShellExecute = false; // tat giao dien
            process.StartInfo.CreateNoWindow = true; // tat tao cua so moi
            process.StartInfo.RedirectStandardOutput = true; // xuat du lieu tieu chuan
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; // an cua so
            
            // chay tien trinh
            process.Start();

            // doc du lieu tra lai cua chuong trinh
            // Stream data = process.StandardOutput.BaseStream;
            */
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            TableDTO selectedTable = lstViewBill.Tag as TableDTO;
            if (IsSelectTable(selectedTable)==true)
            {
                int billID = BillDAO.Instance.GetBillID(selectedTable.MaBan);
                try
                {
                    if (billID != -1) 
                    {
                        if (MessageBox.Show("Mức giảm giá: " + nudPromotion.Value.ToString() + "%\nBạn có chắc muốn thanh toán cho " + selectedTable.TenBan + " không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                        {
                            btnPay.Enabled = false;
                            txbCash.Text = txbTotal.Text; 
                            pnlPrint.Show(); 
                            CalculateSurplus(); 
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Đặt trình in
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            printDialog.Document = printDocument;
            printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(Document);
            //Chọn máy in
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                //Tiến hành in
                printDocument.Print();
            }
            
            //Hoàn tất thanh toán
            Pay();
            //Ẩn panel_print
            pnlPrint.Hide();
            //Tái cho phép truy cập nút thanh toán
            btnPay.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlPrint.Hide();
            btnPay.Enabled = true;
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string tableName = "ten ban tam thoi";
            try
            {
                if (TableDAO.Instance.Insert_table(tableName))
                {
                    ReLoad("1345");
                }
                else
                {
                    MessageBox.Show("Thêm bàn thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            TableDTO selectedTable = lstViewBill.Tag as TableDTO;
            if (IsSelectTable(selectedTable))
            {
                try
                {
                    if (MessageBox.Show("Thao tác này có thể xóa cả hóa đơn đang lưu trên bàn\nHãy chắc rằng bạn muốn xóa", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.No)
                    {
                        List<BillDTO> billIDList = BillDAO.Instance.GetListBillID(selectedTable.MaBan);
                        foreach (BillDTO item in billIDList)
                        {
                            BillInfoDAO.Instance.DeleteBillInfo(item.MaHoaDon);
                            BillDAO.Instance.DeleteBill(item.MaHoaDon);
                        }
                        DeleteTable(selectedTable);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnMoveTable_Click(object sender, EventArgs e)
        {
            TableDTO selectedTable = lstViewBill.Tag as TableDTO;
            try
            {
                if (IsSelectTable(selectedTable))
                {
                    MoveTable(selectedTable);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGatherTable_Click(object sender, EventArgs e)
        {
            TableDTO selectedTable = lstViewBill.Tag as TableDTO;
            if (IsSelectTable(selectedTable))
            {
                GatherTable(selectedTable);
            }
        }

        private void nudPromotion_ValueChanged(object sender, EventArgs e)
        {
            TableDTO selectedTable = lstViewBill.Tag as TableDTO;
            if (selectedTable != null)
            {
                ShowBill(selectedTable.MaBan);
            }
        }

        private void txbCash_TextChanged(object sender, EventArgs e)
        {
            //Khi tiền khách trả thay đổi thì tiền thừa thay đổi theo
            CalculateSurplus();
        }
        #endregion

        #region [ShortcutKey]
        private void thêmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAdd_Click(this, new EventArgs());
        }

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPay_Click(this, new EventArgs());
        }

        private void máyTínhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCalculator_Click(this, new EventArgs());
        }

        #endregion

        #region [ToolStrip]
        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formAdmin f = new formAdmin();
            f.currentAccount = currentAccount;//Truyền tài khoản hiện tại vào formAdmin

            f.Updatestatus_after_addmenu += f_Updatestatus_after_addmenu;
            f.Updatestatus_after_changemenu += f_Updatestatus_after_changemenu;
            f.Updatestatus_after_deletemenu += f_Updatestatus_after_deletemenu;

            f.Updatestatus_after_addcategory += f_Updatestatus_after_addcategory;
            f.Updatestatus_after_deletecategory += f_Updatestatus_after_deletecategory;
            f.Updatestatus_after_changecategory += f_Updatestatus_after_chagecategory;

            f.ShowDialog();
            this.Show();
        }

        private void xemThôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formProfile f = new formProfile(currentAccount);
            f.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region [Update]
        private void f_Updatestatus_after_deletemenu(object sender, EventArgs e)
        {
            LoadDrinksList((cbCategory.SelectedItem as CategoryDTO).MaDanhMuc);
            if (lstViewBill.Tag != null)
                ShowBill((lstViewBill.Tag as TableDTO).MaBan);
            LoadTable();
        }

        private void f_Updatestatus_after_changemenu(object sender, EventArgs e)
        {
            LoadDrinksList((cbCategory.SelectedItem as CategoryDTO).MaDanhMuc);
            if (lstViewBill.Tag != null)
                ShowBill((lstViewBill.Tag as TableDTO).MaBan);
        }

        private void f_Updatestatus_after_addmenu(object sender, EventArgs e)
        {
            LoadDrinksList((cbCategory.SelectedItem as CategoryDTO).MaDanhMuc);
            if (lstViewBill.Tag != null)
                ShowBill((lstViewBill.Tag as TableDTO).MaBan);
        }

        private void f_Updatestatus_after_deletecategory(object sender, EventArgs e)
        {
            LoadCategoryList();
            LoadDrinksList((cbCategory.SelectedItem as CategoryDTO).MaDanhMuc);
            if (lstViewBill.Tag != null)
                ShowBill((lstViewBill.Tag as TableDTO).MaBan);
            LoadTable();
        }

        private void f_Updatestatus_after_chagecategory(object sender, EventArgs e)
        {
            LoadCategoryList();
            LoadDrinksList((cbCategory.SelectedItem as CategoryDTO).MaDanhMuc);
            if (lstViewBill.Tag != null)
                ShowBill((lstViewBill.Tag as TableDTO).MaBan);
        }

        private void f_Updatestatus_after_addcategory(object sender, EventArgs e)
        {
            LoadCategoryList();
            LoadDrinksList((cbCategory.SelectedItem as CategoryDTO).MaDanhMuc);
            if (lstViewBill.Tag != null)
                ShowBill((lstViewBill.Tag as TableDTO).MaBan);
        }
        #endregion
        #endregion
    }
}
