using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZiCoffe.DAO;
using ZiCoffe.DTO;

namespace ZiCoffe.GUI
{
    public partial class formAdmin : Form
    {
        public EmployeesDTO currentAccount;

        BindingSource materialSource = new BindingSource();
        BindingSource categorySource = new BindingSource();
        BindingSource menuSource = new BindingSource();
        BindingSource employeesSource = new BindingSource();
        BindingSource positionSource = new BindingSource();
        BindingSource supplierSource = new BindingSource();

        public formAdmin()
        {
            InitializeComponent();

            LoadDateTimePicker();
            LoadRevenue(dtpStart.Value, dtpEnd.Value);
            DisplayNumRows();

            dtgMaterial.DataSource = materialSource;
            LoadMaterial();

            dtgCategory.DataSource = categorySource;
            LoadCategory();

            LoadCategoryList(cbDrinksCategory);
            dtgMenu.DataSource = menuSource;
            LoadMenu();

            LoadPositionList(cbEmployeesPosition);
            dtgEmployees.DataSource = employeesSource;
            LoadEmployees();

            dtgPosition.DataSource = positionSource;
            LoadPosition();

            LoadSupplierList(cbMaterialSupplier);
            dtgSupplier.DataSource = supplierSource;
            LoadSupplier();

            MaterialBinding();
            CategoryBinding();
            MenuBinding();
            EmployeesBinding();
            PositionBinding();
            SupplierBinding();

            btnReport.Enabled = false;
        }

        #region method

        #region Load
        void LoadDateTimePicker()
        {
            DateTime today = DateTime.Now;
            dtpStart.Value = new DateTime(today.Year, today.Month, 1);
            dtpEnd.Value = dtpStart.Value.AddMonths(1).AddDays(-1);
        }

        void LoadRevenue(DateTime start, DateTime end)
        {
            int pageNumber = Convert.ToInt32(txbPageNumber.Text);
            int maxNumRows = Convert.ToInt32(txbMaxNumRows.Text);
            dtgRevenue.DataSource = BillDAO.Instance.GetRevenue(start, end, pageNumber, maxNumRows);
        }

        void LoadMaterial()
        {
            materialSource.DataSource = MaterialDAO.Instance.GetMaterial();
        }

        void LoadCategory()
        {
            categorySource.DataSource = CategoryDAO.Instance.GetCategory();
        }

        void LoadMenu()
        {
            menuSource.DataSource = MenuDAO.Instance.GetMenu();
        }

        void LoadEmployees()
        {
            employeesSource.DataSource = EmployeesDAO.Instance.GetEmployees();
        }

        void LoadPosition()
        {
            positionSource.DataSource = PositionDAO.Instance.GetPosition();
        }

        void LoadSupplier()
        {
            supplierSource.DataSource = SupplierDAO.Instance.GetSupplier();
        }

        void LoadCategoryList(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetCategoryList();
            cb.DisplayMember = "tendanhmuc";
        }

        void LoadSupplierList(ComboBox cb)
        {
            cb.DataSource = SupplierDAO.Instance.GetSupplierList();
            cb.DisplayMember = "tennhacungcap";
        }

        void LoadPositionList(ComboBox cb)
        {
            cb.DataSource = PositionDAO.Instance.GetPositionList();
            cb.DisplayMember = "tenchucvu";
        }
        #endregion

        #region Binding
        void MaterialBinding()
        {
            txbMaterialID.DataBindings.Add(new Binding("Text", dtgMaterial.DataSource, "Mã nguyên liệu", true, DataSourceUpdateMode.Never));
            txbMaterialName.DataBindings.Add(new Binding("Text", dtgMaterial.DataSource, "Tên nguyên liệu", true, DataSourceUpdateMode.Never));
            cbMaterialSupplier.DataBindings.Add(new Binding("Text", dtgMaterial.DataSource, "Nhà cung cấp", true, DataSourceUpdateMode.Never));
            nudMaterialPrice.DataBindings.Add(new Binding("Value", dtgMaterial.DataSource, "Đơn giá", true, DataSourceUpdateMode.Never));
            txbMaterialUnit.DataBindings.Add(new Binding("Text", dtgMaterial.DataSource, "Đơn vị", true, DataSourceUpdateMode.Never));
            nudMaterialAmount.DataBindings.Add(new Binding("Value", dtgMaterial.DataSource, "Số lượng", true, DataSourceUpdateMode.Never));
            cbMaterialStatus.DataBindings.Add(new Binding("Text", dtgMaterial.DataSource, "Tình trạng", true, DataSourceUpdateMode.Never));
            ricMaterialNote.DataBindings.Add(new Binding("Text", dtgMaterial.DataSource, "Ghi chú", true, DataSourceUpdateMode.Never));
        }
        void CategoryBinding()
        {
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgCategory.DataSource, "Mã danh mục", true, DataSourceUpdateMode.Never));
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgCategory.DataSource, "Tên danh mục", true, DataSourceUpdateMode.Never));
        }
        void MenuBinding()
        {
            txbDrinksID.DataBindings.Add(new Binding("Text", dtgMenu.DataSource, "Mã", true, DataSourceUpdateMode.Never));
            txbDrinkName.DataBindings.Add(new Binding("Text", dtgMenu.DataSource, "Tên đồ uống", true, DataSourceUpdateMode.Never));
            cbDrinksCategory.DataBindings.Add(new Binding("Text", dtgMenu.DataSource, "Tên danh mục", true, DataSourceUpdateMode.Never));
            nudDrinksPrice.DataBindings.Add(new Binding("Value", dtgMenu.DataSource, "Giá bán", true, DataSourceUpdateMode.Never));
            cbDrinkStatus.DataBindings.Add(new Binding("Text", dtgMenu.DataSource, "Tình trạng", true, DataSourceUpdateMode.Never));
            picDrinksImage.DataBindings.Add(new Binding("Image", dtgMenu.DataSource, "Hình ảnh", true, DataSourceUpdateMode.Never));
        }
        void EmployeesBinding()
        {
            txbEmployeesID.DataBindings.Add(new Binding("Text", dtgEmployees.DataSource, "Mã", true, DataSourceUpdateMode.Never));
            txbUsername.DataBindings.Add(new Binding("Text", dtgEmployees.DataSource, "Tên đăng nhập", true, DataSourceUpdateMode.Never));
            txbEmployeesName.DataBindings.Add(new Binding("Text", dtgEmployees.DataSource, "Họ tên", true, DataSourceUpdateMode.Never));
            cbEmployeesPosition.DataBindings.Add(new Binding("Text", dtgEmployees.DataSource, "Chức vụ", true, DataSourceUpdateMode.Never));
            cbSex.DataBindings.Add(new Binding("Text", dtgEmployees.DataSource, "Giới tính", true, DataSourceUpdateMode.Never));
            txbIDNum.DataBindings.Add(new Binding("Text", dtgEmployees.DataSource, "CMND", true, DataSourceUpdateMode.Never));
            txbEmployeesAddress.DataBindings.Add(new Binding("Text", dtgEmployees.DataSource, "Địa chỉ", true, DataSourceUpdateMode.Never));
            txbEmployeesPhone.DataBindings.Add(new Binding("Text", dtgEmployees.DataSource, "SĐT", true, DataSourceUpdateMode.Never));
            picAvatar.DataBindings.Add(new Binding("Image", dtgEmployees.DataSource, "Ảnh", true, DataSourceUpdateMode.Never));
        }
        void PositionBinding()
        {
            txbPositionID.DataBindings.Add(new Binding("Text", dtgPosition.DataSource, "Mã", true, DataSourceUpdateMode.Never));
            txbPositionName.DataBindings.Add(new Binding("Text", dtgPosition.DataSource, "Chức vụ", true, DataSourceUpdateMode.Never));
        }
        void SupplierBinding()
        {
            txbSupplierID.DataBindings.Add(new Binding("Text", dtgSupplier.DataSource, "Mã", true, DataSourceUpdateMode.Never));
            txbSupplierName.DataBindings.Add(new Binding("Text", dtgSupplier.DataSource, "Tên nhà cung cấp", true, DataSourceUpdateMode.Never));
            txbSupplierAddress.DataBindings.Add(new Binding("Text", dtgSupplier.DataSource, "Địa chỉ", true, DataSourceUpdateMode.Never));
            txbSupplierPhone.DataBindings.Add(new Binding("Text", dtgSupplier.DataSource, "SĐT", true, DataSourceUpdateMode.Never));
            txbSupplierEmail.DataBindings.Add(new Binding("Text", dtgSupplier.DataSource, "Email", true, DataSourceUpdateMode.Never));
            ricSupplierNote.DataBindings.Add(new Binding("Text", dtgSupplier.DataSource, "Ghi chú", true, DataSourceUpdateMode.Never));
        }
        #endregion

        #region Other
        byte[] ConvertImageToBytes(string imagePath)
        {
            if (String.IsNullOrEmpty(imagePath))
            {
                return null;
            }
            else
            {
                FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                byte[] binaryArray = new byte[fs.Length];
                fs.Read(binaryArray, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                return binaryArray;
            }
        }

        void DisplayNumRows()
        {
            DateTime start = dtpStart.Value;
            DateTime end = dtpEnd.Value;
            int pageNum = Convert.ToInt32(txbPageNumber.Text);
            int maxNumRows = Convert.ToInt32(txbMaxNumRows.Text);
            txbDisplayNumRows.Text = BillDAO.Instance.GetDisPlayRecord(start, end, pageNum, maxNumRows).ToString();
        }

        int GetLastPage()
        {
            int totalRecord = BillDAO.Instance.GetRevenueRecordNum(dtpStart.Value, dtpEnd.Value);
            int maxRow = Convert.ToInt32(txbMaxNumRows.Text);
            int lastPage = totalRecord / maxRow;
            if (totalRecord % maxRow != 0) // 11 record / 10 maxrowsinpage = 2 page
                return lastPage++;
            return lastPage;
        }

        int GetSupplierID()
        {
            List<SupplierDTO> supplierList = SupplierDAO.Instance.GetSupplierList();
            foreach (SupplierDTO item in supplierList)
            {
                if (cbMaterialSupplier.Text == item.TenNhaCungCap)
                    return item.MaNhaCungCap;
            }
            return -1;
        }
        #endregion

        #endregion

        #region Event
        #region [E] Revenue
        private void btnView_Click(object sender, EventArgs e)
        {
            LoadRevenue(dtpStart.Value, dtpEnd.Value);
            DisplayNumRows();
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            txbPageNumber.Text = "1";
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            int currentPage = Convert.ToInt32(txbPageNumber.Text);
            if (currentPage > 1)
                currentPage--;
            txbPageNumber.Text = currentPage.ToString();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            int currentPage = Convert.ToInt32(txbPageNumber.Text);
            int lastPage = GetLastPage();
            if (currentPage < lastPage)
                currentPage++;
            txbPageNumber.Text = currentPage.ToString();
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            int lastPage = GetLastPage();
            txbPageNumber.Text = lastPage.ToString();
        }

        private void txbPageNumber_TextChanged(object sender, EventArgs e)
        {
            dtgRevenue.DataSource = BillDAO.Instance.GetRevenue(dtpStart.Value, dtpEnd.Value, Convert.ToInt32(txbPageNumber.Text), Convert.ToInt32(txbMaxNumRows.Text));
        }
        #endregion

        #region [E] Category
        private void txbSearchCategory_Click(object sender, EventArgs e)
        {
            txbSearchCategory.Clear();
        }

        private void btnSearchCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txbSearchCategory.Text;
            categorySource.DataSource = CategoryDAO.Instance.SearchCategory(categoryName);
        }

        private void btnClearCategory_Click(object sender, EventArgs e)
        {
            txbCategoryID.Clear();
            txbCategoryName.Clear();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txbCategoryName.Text;
            try
            {
                if (CategoryDAO.Instance.AddCategory(categoryName))
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategory();
                    LoadCategoryList(cbDrinksCategory);
                    if (updatestatus_after_addcategory != null)
                    {
                        updatestatus_after_addcategory(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int categoryID = Convert.ToInt32(txbCategoryID.Text);
            try
            {
                if (CategoryDAO.Instance.DeleteCategory(categoryID))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategory();
                    LoadMenu();
                    LoadCategoryList(cbDrinksCategory);
                    LoadDateTimePicker();
                    LoadRevenue(dtpStart.Value, dtpEnd.Value);
                    if (updatestatus_after_deletecategory != null)
                    {
                        updatestatus_after_deletecategory(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txbCategoryName.Text;
            int categoryID = Convert.ToInt32(txbCategoryID.Text);
            try
            {
                if (CategoryDAO.Instance.ChangeCategory(categoryName, categoryID))
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategory();
                    LoadMenu();
                    LoadCategoryList(cbDrinksCategory);
                    LoadDateTimePicker();
                    LoadRevenue(dtpStart.Value, dtpEnd.Value);
                    if (updatestatus_after_changecategory != null)
                    {
                        updatestatus_after_changecategory(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Sửa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private event EventHandler updatestatus_after_addcategory;
        public event EventHandler Updatestatus_after_addcategory
        {
            add { updatestatus_after_addcategory += value; }
            remove { updatestatus_after_addcategory -= value; }
        }

        private event EventHandler updatestatus_after_deletecategory;
        public event EventHandler Updatestatus_after_deletecategory
        {
            add { updatestatus_after_deletecategory += value; }
            remove { updatestatus_after_deletecategory -= value; }
        }

        private event EventHandler updatestatus_after_changecategory;
        public event EventHandler Updatestatus_after_changecategory
        {
            add { updatestatus_after_changecategory += value; }
            remove { updatestatus_after_changecategory -= value; }
        }


        #endregion

        #region [E] Menu
        private void txbSearchMenu_Click(object sender, EventArgs e)
        {
            txbSearchMenu.Clear();
        }

        private void btnSearchMenu_Click(object sender, EventArgs e)
        {
            string drinksName = txbSearchMenu.Text;
            menuSource.DataSource = MenuDAO.Instance.SearchMenu(drinksName);
        }

        private void btnClearMenu_Click(object sender, EventArgs e)
        {
            txbDrinksID.Clear();
            txbDrinkName.Clear();
            nudDrinksPrice.Value = 0;
            txbDrinksPath.Text = Application.StartupPath + "\\Resources\\no_image.png";
            picDrinksImage.Image = Image.FromFile(Application.StartupPath + "\\Resources\\no_image.png");
        }

        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            string drinksName = txbDrinkName.Text;
            int categoryID = (cbDrinksCategory.SelectedItem as CategoryDTO).MaDanhMuc;
            float price = (float)nudDrinksPrice.Value;
            int status = (cbDrinkStatus.Text == "Hết") ? 0 : 1;
            byte[] image = ConvertImageToBytes(txbDrinksPath.Text);
            try
            {
                if (MenuDAO.Instance.AddDrinks(drinksName, categoryID, price, status, image))
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMenu();
                    if (updatestatus_after_addmenu != null)
                    {
                        updatestatus_after_addmenu(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteMenu_Click(object sender, EventArgs e)
        {
            int drinksID = Convert.ToInt32(txbDrinksID.Text);
            try
            {
                if (MenuDAO.Instance.DeleteDrinks(drinksID))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMenu();
                    if (updatestatus_after_deletemenu != null)
                    {
                        updatestatus_after_deletemenu(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeMenu_Click(object sender, EventArgs e)
        {
            string drinksName = txbDrinkName.Text;
            int categoryID = (cbDrinksCategory.SelectedItem as CategoryDTO).MaDanhMuc;
            float price = (float)nudDrinksPrice.Value;
            int status = (cbDrinkStatus.Text == "Hết") ? 0 : 1;
            byte[] image = ConvertImageToBytes(txbDrinksPath.Text);
            int drinksID = Convert.ToInt32(txbDrinksID.Text);
            try
            {
                if(image!=null)
                {
                    if (MenuDAO.Instance.ChangeDrinks(drinksName, categoryID, price, status, image, drinksID))
                    {
                        MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMenu();
                        if (updatestatus_after_changemenu != null)
                        {
                            updatestatus_after_changemenu(this, new EventArgs());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sửa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (MenuDAO.Instance.ChangeDrinks2(drinksName, categoryID, price, status, drinksID))
                    {
                        MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMenu();
                        if (updatestatus_after_changemenu != null)
                        {
                            updatestatus_after_changemenu(this, new EventArgs());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateMenu_Click(object sender, EventArgs e)
        {
            LoadMenu();
        }

        private void btnChooseDrinksImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string fileName = openFileDialog1.FileName;
            if (string.IsNullOrEmpty(fileName))
                return;
            Image drinksPicture = Image.FromFile(fileName);
            picDrinksImage.Image = drinksPicture;
            txbDrinksPath.Text = fileName;
        }

        private void btnCancelDrinksImage_Click(object sender, EventArgs e)
        {
            txbDrinksPath.Text = Application.StartupPath + "\\Resources\\no_image.png";
            picDrinksImage.Image = Image.FromFile(Application.StartupPath + "\\Resources\\no_image.png");
        }

        private event EventHandler updatestatus_after_addmenu;
        public event EventHandler Updatestatus_after_addmenu
        {
            add { updatestatus_after_addmenu += value; }
            remove { updatestatus_after_addmenu -= value; }
        }

        private event EventHandler updatestatus_after_deletemenu;
        public event EventHandler Updatestatus_after_deletemenu
        {
            add { updatestatus_after_deletemenu += value; }
            remove { updatestatus_after_deletemenu -= value; }
        }

        private event EventHandler updatestatus_after_changemenu;
        public event EventHandler Updatestatus_after_changemenu
        {
            add { updatestatus_after_changemenu += value; }
            remove { updatestatus_after_changemenu -= value; }
        }
        #endregion

        #region [E] Material
        private void txbSearchMaterial_Click(object sender, EventArgs e)
        {
            txbSearchMaterial.Clear();
        }

        private void btnSearchMaterial_Click(object sender, EventArgs e)
        {
            string materialName = txbSearchMaterial.Text;
            materialSource.DataSource = MaterialDAO.Instance.SearchMaterial(materialName);
        }

        private void btnClearMaterial_Click(object sender, EventArgs e)
        {
            txbMaterialID.Clear();
            txbMaterialName.Clear();
            nudMaterialPrice.Value = 0;
            txbMaterialUnit.Clear();
            nudMaterialAmount.Value = 0;
            ricMaterialNote.Clear();
        }

        private void btnAddMaterial_Click(object sender, EventArgs e)
        {
            string materialName = txbMaterialName.Text;
            int supplierID = GetSupplierID();
            float price = (float)nudMaterialPrice.Value;
            string unit = txbMaterialUnit.Text;
            int amount = (int)nudMaterialAmount.Value;
            string note = ricMaterialNote.Text;
            int status = (cbMaterialStatus.Text == "Hết") ? 0 : 1;
            try
            {
                if (supplierID != -1)
                {
                    if (MaterialDAO.Instance.AddMaterial(status))
                    {
                        int materialID = MaterialDAO.Instance.GetNewMaterialID();
                        if (MaterialInfoDAO.Instance.AddMaterialInfo(materialID, materialName, supplierID, price, unit, amount, note))
                        {
                            MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadMaterial();
                        }
                        else
                        {
                            MessageBox.Show("Thêm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteMaterial_Click(object sender, EventArgs e)
        {
            int materialID = Convert.ToInt32(txbMaterialID.Text);
            try
            {
                if (MaterialInfoDAO.Instance.DeleteMaterialInfo(materialID))
                {
                    if (MaterialDAO.Instance.DeleteMaterial(materialID))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMaterial();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeMaterial_Click(object sender, EventArgs e)
        {
            string materialName = txbMaterialName.Text;
            int supplierID = GetSupplierID();
            float price = (float)nudMaterialPrice.Value;
            string unit = txbMaterialUnit.Text;
            int amount = (int)nudMaterialAmount.Value;
            string note = ricMaterialNote.Text;
            int status = (cbMaterialStatus.Text == "Hết") ? 0 : 1;
            int materialID = Convert.ToInt32(txbMaterialID.Text);
            try
            {
                if (supplierID != -1)
                {
                    if (MaterialDAO.Instance.ChangeMaterial(status, materialID))
                    {
                        if (MaterialInfoDAO.Instance.ChangeMaterialInfo(materialID, materialName, supplierID, price, unit, amount, note))
                        {
                            MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadMaterial();
                        }
                        else
                        {
                            MessageBox.Show("Sửa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateMaterial_Click(object sender, EventArgs e)
        {
            LoadMaterial();
        }
        #endregion

        #region [E] Supplier
        private void txbSearchSupplier_Click(object sender, EventArgs e)
        {
            txbSearchSupplier.Clear();
        }

        private void btnSearchSupplier_Click(object sender, EventArgs e)
        {
            string supplierName = txbSearchSupplier.Text;
            supplierSource.DataSource = SupplierDAO.Instance.SearchSupplier(supplierName);
        }

        private void btnClearSupplier_Click(object sender, EventArgs e)
        {
            txbSupplierID.Clear();
            txbSupplierName.Clear();
            txbSupplierAddress.Clear();
            txbSupplierPhone.Clear();
            txbSupplierEmail.Clear();
            ricSupplierNote.Clear();
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            string supplierName = txbSupplierName.Text;
            string address = txbSupplierAddress.Text;
            string phone = txbSupplierPhone.Text;
            string email = txbSupplierEmail.Text;
            string note = ricSupplierNote.Text;
            try
            {
                if (SupplierDAO.Instance.AddSupplier(supplierName, address, phone, email, note))
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSupplier();
                    LoadSupplierList(cbMaterialSupplier);
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteSupplier_Click(object sender, EventArgs e)
        {
            int supplierID = Convert.ToInt32(txbSupplierID.Text);
            try
            {
                if (SupplierDAO.Instance.DeleteSupplier(supplierID))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSupplier();
                    LoadSupplierList(cbMaterialSupplier);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeSupplier_Click(object sender, EventArgs e)
        {
            string supplierName = txbSupplierName.Text;
            string address = txbSupplierAddress.Text;
            string phone = txbSupplierPhone.Text;
            string email = txbSupplierEmail.Text;
            string note = ricSupplierNote.Text;
            int supplierID = Convert.ToInt32(txbSupplierID.Text);
            try
            {
                if (SupplierDAO.Instance.ChangeSupplier(supplierName, address, phone, email, note, supplierID))
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSupplier();
                    LoadSupplierList(cbMaterialSupplier);
                }
                else
                {
                    MessageBox.Show("Sửa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateSupplier_Click(object sender, EventArgs e)
        {
            LoadSupplier();
        }
        #endregion

        #region [E] Position
        private void txbSearchPosition_Click(object sender, EventArgs e)
        {
            txbSearchPosition.Clear();
        }

        private void btnSearchPosition_Click(object sender, EventArgs e)
        {
            string positionName = txbSearchPosition.Text;
            positionSource.DataSource = PositionDAO.Instance.SearchPosition(positionName);
        }

        private void btnClearPosition_Click(object sender, EventArgs e)
        {
            txbPositionID.Clear();
            txbPositionName.Clear();
        }

        private void btnAddPosition_Click(object sender, EventArgs e)
        {
            string positionName = txbPositionName.Text;
            try
            {
                if (PositionDAO.Instance.AddPosition(positionName))
                {
                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPosition();
                    LoadPositionList(cbEmployeesPosition);
                }
                else
                {
                    MessageBox.Show("Thêm thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeletePosition_Click(object sender, EventArgs e)
        {
            int positionID = Convert.ToInt32(txbPositionID.Text);
            if (positionID == 1 || positionID == 2)
            {
                MessageBox.Show("Đây là cơ sở mặc định\nKhông thể xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (MessageBox.Show("Thao tác này có thể xóa các tài khoản đang giữ chức vụ tương ứng\nHãy chắc chắn bạn muốn xóa", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.No)
                    {

                        if (PositionDAO.Instance.DeletePosition(positionID))
                        {
                            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadPosition();
                            LoadPositionList(cbEmployeesPosition);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnChangePosition_Click(object sender, EventArgs e)
        {
            int positionID = Convert.ToInt32(txbPositionID.Text);
            string positionName = txbPositionName.Text;
            if (positionID == 1)
            {
                MessageBox.Show("Đây là cấp quản trị\nKhông thể thay đổi", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (PositionDAO.Instance.ChangePosition(positionName, positionID))
                    {
                        MessageBox.Show("Sửa thành công", "Thông báo");
                        LoadPosition();
                        LoadPositionList(cbEmployeesPosition);
                    }
                    else
                    {
                        MessageBox.Show("Sửa thất bại", "Thông báo");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUpdatePosition_Click(object sender, EventArgs e)
        {
            LoadPosition();
        }
        #endregion

        #region [E] Employees
        private void txbSearchEmployees_Click(object sender, EventArgs e)
        {
            txbSearchEmployees.Clear();
        }

        private void btnSearchEmployees_Click(object sender, EventArgs e)
        {
            string empoyeesName = txbEmployeesName.Text;
            employeesSource.DataSource = EmployeesDAO.Instance.SearchEmployees(empoyeesName);
        }

        private void btnDeleteEmployees_Click(object sender, EventArgs e)
        {
            string accountName = txbUsername.Text.ToString();
            try
            {
                if (currentAccount.TenDangNhap.Equals(accountName))
                {
                    MessageBox.Show("Đây là tài khoản hiện hành\nHãy dùng tài khoản khác có cấp tương đương hoặc hơn để xóa", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (EmployeesDAO.Instance.DeleteEmployees2(accountName))
                {
                    if (LoginDAO.Instance.DeleteAccount(accountName))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployees();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangeEmployees_Click(object sender, EventArgs e)
        {
            string name = txbEmployeesName.Text;
            string idNum = txbIDNum.Text;
            string address = txbEmployeesAddress.Text;
            string phone = txbEmployeesPhone.Text;
            string accountName = txbUsername.Text;

            byte[] avatar = ConvertImageToBytes(txbAvatarPath.Text);
            string position = cbEmployeesPosition.Text;
            int positionCode = 1;
            List<PositionDTO> positionList = PositionDAO.Instance.GetPositionList();
            foreach (PositionDTO item in positionList)
            {
                if (position == item.TenChucVu)
                    positionCode = item.MaChucVu;
            }
            string sex = cbSex.Text;
            int sexCode = 0;
            if (sex == "Nữ")
                sexCode = 1;
            try
            {
                if (avatar != null)
                {
                    if (EmployeesDAO.Instance.ChangeEmployees(name, idNum, address, phone, sexCode, positionCode, avatar, accountName))
                    {
                        MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployees();
                    }
                    else
                    {
                        MessageBox.Show("Sửa thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (EmployeesDAO.Instance.ChangeEmployees2(name, idNum, address, phone, sexCode, positionCode, accountName))
                    {
                        MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployees();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateEmployees_Click(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            formNewEmloyees f = new formNewEmloyees();
            f.ShowDialog();
            LoadEmployees();
            this.Show();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string accountName = txbUsername.Text;
            try
            {
                if (LoginDAO.Instance.ResetPassword(accountName))
                {
                    MessageBox.Show("Đặt lại mật khẩu thành công\nMật khẩu hiện tại của bạn là: 1 ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Đặt lại mật khẩu thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChooseAvatar_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string fileName = openFileDialog1.FileName;
            if (string.IsNullOrEmpty(fileName))
                return;
            Image avatar = Image.FromFile(fileName);
            picAvatar.Image = avatar;
            txbAvatarPath.Text = fileName;
            MessageBox.Show("Nhấn sửa để cập nhật ảnh đại diện\nnếu không muốn có thể bỏ qua bước này", "Hướng dẫn");
        }

        private void btnCancelAvatar_Click(object sender, EventArgs e)
        {
            txbAvatarPath.Text = Application.StartupPath + "\\Resources\\Noavatar.jpg";
            picAvatar.Image = Image.FromFile(Application.StartupPath + "\\Resources\\Noavatar.jpg");
        }
        #endregion
        #endregion
    }
}
