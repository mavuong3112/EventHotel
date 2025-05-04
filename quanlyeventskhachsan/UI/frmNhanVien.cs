using System;
using System.Linq;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;
using Guna.UI2.WinForms;
using quanlyeventskhachsan.MODEL;

namespace quanlyeventskhachsan.UI 
{
    public partial class frmNhanVien : Form
    {
        private Context dbContext;

        public frmNhanVien()
        {
            InitializeComponent();
            dbContext = new Context();
        }

        private void gbNhanVien_Enter(object sender, EventArgs e)
        {
            // Nếu có mã nguồn cho sự kiện này, bạn có thể thêm mã vào đây
        }

        public bool FindDuplicateMSNV()
        {
            foreach (DataGridViewRow row in dgvNhanVien.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["MSNV"].Value != null && row.Cells["MSNV"].Value.ToString() == txtMSNV.Text.Trim())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ValidateNV()
        {
            if (txtMSNV.Text.Length == 0)
            {
                MessageBox.Show("MSNV Đang Trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (FindDuplicateMSNV())
            {
                MessageBox.Show("MSNV Đang Bị Trùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        public void clearNVFields()
        {
            txtMSNV.Clear();
            txtName.Clear();
            cbCoSo.SelectedIndex = -1;
            txtMSNV.ReadOnly = false;
            //btnAddNV.Enabled = true;
        }

        private void btnAddNV_Click(object sender, EventArgs e)
        {
            if (ValidateNV())
            {     
                // Lưu dữ liệu vào cơ sở dữ liệu
                AddDataToDatabase(txtMSNV.Text, txtName.Text, cbCoSo.SelectedValue.ToString().Trim());
                //Load db
                LoadDataToDGV();

                // Xóa các trường dữ liệu
                clearNVFields();
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
            }
        }

        private void AddDataToDatabase(string msnv, string hoTen, string coSo)
        {
            msnv = msnv.Trim(); hoTen = hoTen.Trim();
            try
            {
                using (Context dbContext = new Context())
                {
                    // Tạo một đối tượng NHAN_VIEN mới
                    NHAN_VIEN newNhanVien = new NHAN_VIEN
                    {
                        MSNV = msnv,
                        HoTen = hoTen,
                        CoSo = coSo,
                        Hide = false,

                        // Nếu có thêm các trường khác, hãy thêm vào đây
                    };

                    // Thêm đối tượng mới vào DbSet và lưu vào cơ sở dữ liệu
                    dbContext.NHAN_VIEN.Add(newNhanVien);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed when AddDataToDatabase.\n**Detail**\n\n" + ex.Message,"Error, Contact admin");
            }
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            // Load dữ liệu vào DataGridView
            LoadDataToDGV();
            LoadMaCoSoToComboBox();
            LoadAutoComplete();
            btnTimNV.Enabled = false;
        }
        private void LoadMaCoSoToComboBox()
        {
            try
            {
                using (Context db = new Context())
                {
                    var coso = from cs in db.CO_SOs
                               where (cs.Hide == false)
                               select new
                               {
                                   MaCS = cs.MaCoSo,
                                   Ten = cs.TenCoSo,
                               };
                    cbCoSo.DataSource = coso.ToList();
                    cbCoSo.DisplayMember = "Ten";
                    cbCoSo.ValueMember = "MaCS";
                    cbCoSo.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Cơ Sở:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDataToDGV()
        {
            dgvNhanVien.Rows.Clear();
            dgvNhanVien.Refresh();
            try
            {
                using (Context dbContext = new Context())
                {
                    // Truy vấn LINQ để lấy dữ liệu từ bảng NHAN_VIEN
                    var nhanVienData = (from nv in dbContext.NHAN_VIEN
                                       where (nv.Hide == false)
                                       select new
                                       {
                                           MSNV = nv.MSNV,
                                           HoTen = nv.HoTen,
                                           coSo = nv.CoSo,
                                           TenCoSo = nv.CO_SO1.TenCoSo,
                                       }).Take(500);

                    // Gán dữ liệu cho DataGridView dgvNhanVien
                    foreach (var nv in nhanVienData)
                    {
                        dgvNhanVien.Rows.Add(nv.MSNV,nv.HoTen,nv.coSo,nv.TenCoSo);
                    }    
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi chi tiết
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu Nhân Viên.\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reset()
        {
            txtMSNV.Text = txtName.Text = cbCoSo.Text = "";
        }
 
        private void btnNVEdit_Click(object sender, EventArgs e)
        {
            try
            {
                using (Context dbContext = new Context())
                {
                    // Lấy thông tin từ các TextBox và ComboBox
                    string msnv = txtMSNV.Text.Trim();
                    string hoTen = txtName.Text.Trim();
                    string coSo = cbCoSo.SelectedValue.ToString().Trim();
                    string tenCoSo = cbCoSo.Text.Trim();

                    // Kiểm tra xem MSNV có tồn tại trong cơ sở dữ liệu không
                    NHAN_VIEN nhanVienToUpdate = dbContext.NHAN_VIEN.FirstOrDefault(nv => nv.MSNV == msnv);

                    if (nhanVienToUpdate != null)
                    {
                        // Cập nhật thông tin nhân viên
                        nhanVienToUpdate.HoTen = hoTen;
                        nhanVienToUpdate.CoSo = coSo;

                        // Lưu thay đổi vào cơ sở dữ liệu
                        dbContext.SaveChanges();

                        MessageBox.Show("Cập nhật thông tin nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataToDGV();

                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên cần cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed when btnNVEdit_Click.\n**Detail**\n\n" + ex.Message, "Error, Contact admin");
            }
        }

        private void import_excel(object sender, EventArgs e)
        {
            // Sử dụng OpenFileDialog để chọn tệp Excel
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls|All Files|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Đường dẫn đến tệp Excel đã chọn
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        // Sử dụng thư viện EPPlus để đọc dữ liệu từ tệp Excel
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                        {
                            // Lấy sheet đầu tiên (index 0)
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                            int rowCount = worksheet.Dimension.Rows;

                            // Bắt đầu từ dòng thứ 2 để bỏ qua dòng tiêu đề
                            for (int row = 2; row <= rowCount; row++)
                            {
                                // Lấy dữ liệu từ từng ô trong dòng
                                string msnv = worksheet.Cells[row, 1].Value?.ToString();
                                string hoTen = worksheet.Cells[row, 2].Value?.ToString();
                                string coSo = worksheet.Cells[row, 3].Value?.ToString();

                                // Kiểm tra và thêm nhân viên mới vào cơ sở dữ liệu
                                AddOrUpdateNhanVien(msnv, hoTen, coSo);
                            }

                            // Cập nhật lại dgvNhanVien sau khi thêm nhân viên mới
                            LoadDataToDGV();
                        }

                        MessageBox.Show("Import dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi đọc dữ liệu từ tệp Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void AddOrUpdateNhanVien(string msnv, string hoTen, string coSo)
        {
            // Kiểm tra xem nhân viên đã tồn tại trong cơ sở dữ liệu chưa
            NHAN_VIEN existingNhanVien = dbContext.NHAN_VIEN.FirstOrDefault(nv => nv.MSNV == msnv);

            if (existingNhanVien == null)
            {
                // Nếu chưa tồn tại, thêm nhân viên mới vào cơ sở dữ liệu
                NHAN_VIEN newNhanVien = new NHAN_VIEN
                {
                    MSNV = msnv,
                    HoTen = hoTen,
                    CoSo = coSo
                };

                dbContext.NHAN_VIEN.Add(newNhanVien);
            }
            else
            {
                // Nếu đã tồn tại, cập nhật thông tin của nhân viên
                existingNhanVien.HoTen = hoTen;
                existingNhanVien.CoSo = coSo;
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            dbContext.SaveChanges();
        }


        private void dgvNhanVien_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có dòng được chọn không và có phải là dòng dữ liệu hay không
            if (e.RowIndex >= 0 && e.RowIndex < dgvNhanVien.Rows.Count - 1)
            {
                DataGridViewRow selectedRow = dgvNhanVien.Rows[e.RowIndex];

                // Lấy thông tin từ dòng được chọn
                string msnv = selectedRow.Cells["MSNV"].Value.ToString();
                string hoTen = selectedRow.Cells["HoTen"].Value.ToString();
                string cs = selectedRow.Cells["CoSo"].Value.ToString();

                // Hiển thị thông tin trong các TextBox và ComboBox
                txtMSNV.Text = msnv;
                txtName.Text = hoTen;
                cbCoSo.SelectedValue = cs;
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên {dgvNhanVien.SelectedRows[0].Cells["MSNV"].Value.ToString()} không?", "Xác nhận xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    // Kiểm tra xem có dòng được chọn trong dgvNhanVien không
                    if (dgvNhanVien.SelectedRows.Count > 0)
                    {
                        // Lặp qua các dòng được chọn và xóa nhân viên khỏi cơ sở dữ liệu
                        foreach (DataGridViewRow selectedRow in dgvNhanVien.SelectedRows)
                        {
                            // Lấy thông tin từ dòng được chọn
                            string msnv = selectedRow.Cells["MSNV"].Value.ToString();

                            // Xóa nhân viên từ cơ sở dữ liệu
                            DeleteNhanVien(msnv);
                        }

                        // Cập nhật lại dgvNhanVien sau khi xóa nhân viên
                        LoadDataToDGV();
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn nhân viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                return;
        }

        private void DeleteNhanVien(string msnv)
        {
            using (Context dbContext = new Context())
            {

                    // Lấy danh sách nhân viên cần xóa
                    NHAN_VIEN nhanVienToDelete = dbContext.NHAN_VIEN.Where(nv => nv.MSNV == msnv).FirstOrDefault();

                        nhanVienToDelete.Hide = true;
                dbContext.Entry(nhanVienToDelete).State = System.Data.Entity.EntityState.Modified;
                // Lưu thay đổi vào cơ sở dữ liệu
                dbContext.SaveChanges();

                    MessageBox.Show("Xóa nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataToDGV();
            }
        }



        private void btn_Export(object sender, EventArgs e)
        {
            // Create SaveFileDialog to choose the location to save the Excel file
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx|All Files|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        // Create a new Excel package
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        using (ExcelPackage package = new ExcelPackage())
                        {
                            // Add a new worksheet to the Excel package
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("NhanVienData");

                            // Write headers to the worksheet
                            for (int col = 1; col <= dgvNhanVien.Columns.Count; col++)
                            {
                                worksheet.Cells[1, col].Value = dgvNhanVien.Columns[col - 1].HeaderText;
                            }

                            // Write data from DataGridView to the worksheet
                            for (int row = 0; row < dgvNhanVien.Rows.Count; row++)
                            {
                                for (int col = 0; col < dgvNhanVien.Columns.Count; col++)
                                {
                                    worksheet.Cells[row + 2, col + 1].Value = dgvNhanVien.Rows[row].Cells[col].Value;
                                }
                            }
                            worksheet.Cells.AutoFitColumns();
                            // Save the Excel package to the selected file
                            package.SaveAs(new FileInfo(filePath));

                            MessageBox.Show("Export successful.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting data to Excel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void btn_Find(object sender, EventArgs e)
        {
            // Get the search keyword from txtboxes

            // Check if the search keyword is empty
            if (string.IsNullOrEmpty(txtMSNV.Text.Trim()) && string.IsNullOrEmpty(txtName.Text.Trim()) && cbCoSo.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập dữ liệu để tìm", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                dgvNhanVien.Rows.Clear();
                dgvNhanVien.Refresh();
                //Tìm theo MSNV
                if (!string.IsNullOrEmpty(txtMSNV.Text.Trim()) && string.IsNullOrEmpty(txtName.Text.Trim()) && cbCoSo.SelectedIndex == -1)
                {
                    string Msnv = txtMSNV.Text.Trim();
                    using (Context dbContext = new Context())
                    {
                        // Truy vấn LINQ để lấy dữ liệu từ bảng NHAN_VIEN
                        var nhanVienData = from nv in dbContext.NHAN_VIEN
                                           where (nv.Hide == false) && nv.MSNV.Contains(Msnv)
                                           select new
                                           {
                                               MSNV = nv.MSNV,
                                               HoTen = nv.HoTen,
                                               CoSo = nv.CoSo,
                                               TenCoSo = nv.CO_SO1.TenCoSo,
                                           };

                        // Gán dữ liệu cho DataGridView dgvNhanVien
                        foreach (var nv in nhanVienData)
                        {
                            dgvNhanVien.Rows.Add(nv.MSNV, nv.HoTen, nv.CoSo, nv.TenCoSo);
                        }
                    }
                }  
                //Tìm theo Họ Tên
                else if (string.IsNullOrEmpty(txtMSNV.Text.Trim()) && !string.IsNullOrEmpty(txtName.Text.Trim()) && cbCoSo.SelectedIndex == -1)
                {
                    string hoTen = txtName.Text.Trim();
                    using (Context dbContext = new Context())
                    {
                        // Truy vấn LINQ để lấy dữ liệu từ bảng NHAN_VIEN
                        var nhanVienData = from nv in dbContext.NHAN_VIEN
                                           where (nv.Hide == false) && nv.HoTen.Contains(hoTen)
                                           select new
                                           {
                                               MSNV = nv.MSNV,
                                               HoTen = nv.HoTen,
                                               CoSo = nv.CoSo,
                                               TenCoSo = nv.CO_SO1.TenCoSo,
                                           };

                        // Gán dữ liệu cho DataGridView dgvNhanVien
                        foreach (var nv in nhanVienData)
                        {
                            dgvNhanVien.Rows.Add(nv.MSNV, nv.HoTen, nv.CoSo, nv.TenCoSo);
                        }
                    }
                }
                //Tìm theo Khoa
                else if (string.IsNullOrEmpty(txtMSNV.Text.Trim()) && string.IsNullOrEmpty(txtName.Text.Trim()) && cbCoSo.SelectedIndex != -1)
                {
                    string MaCoSo = cbCoSo.SelectedValue.ToString();
                    using (Context dbContext = new Context())
                    {
                        // Truy vấn LINQ để lấy dữ liệu từ bảng NHAN_VIEN
                        var nhanVienData = from nv in dbContext.NHAN_VIEN
                                           where (nv.Hide == false) && nv.CoSo == MaCoSo
                                           select new
                                           {
                                               MSNV = nv.MSNV,
                                               HoTen = nv.HoTen,
                                               CoSo = nv.CoSo,
                                               TenCoSo = nv.CO_SO1.TenCoSo,
                                           };

                        // Gán dữ liệu cho DataGridView dgvNhanVien
                        foreach (var nv in nhanVienData)
                        {
                            dgvNhanVien.Rows.Add(nv.MSNV, nv.HoTen, nv.CoSo, nv.TenCoSo);
                        }
                    }
                }
                //Tìm theo MSNV và Cơ sở
                else if (!string.IsNullOrEmpty(txtMSNV.Text.Trim()) && string.IsNullOrEmpty(txtName.Text.Trim()) && cbCoSo.SelectedIndex != -1)
                {
                    string Msnv = txtMSNV.Text.Trim();
                    string MaCoSo = cbCoSo.SelectedValue.ToString();
                    using (Context dbContext = new Context())
                    {
                        // Truy vấn LINQ để lấy dữ liệu từ bảng NHAN_VIEN
                        var nhanVienData = from nv in dbContext.NHAN_VIEN
                                           where (nv.Hide == false) && nv.CoSo == MaCoSo && nv.MSNV.Contains(Msnv)
                                           select new
                                           {
                                               MSNV = nv.MSNV,
                                               HoTen = nv.HoTen,
                                               CoSo = nv.CoSo,
                                               TenCoSo = nv.CO_SO1.TenCoSo,
                                           };

                        // Gán dữ liệu cho DataGridView dgvNhanVien
                        foreach (var nv in nhanVienData)
                        {
                            dgvNhanVien.Rows.Add(nv.MSNV, nv.HoTen, nv.CoSo, nv.TenCoSo);
                        }
                    }
                }
                //Tìm theo Khoa và Họ Tên
                else if (string.IsNullOrEmpty(txtMSNV.Text.Trim()) && !string.IsNullOrEmpty(txtName.Text.Trim()) && cbCoSo.SelectedIndex != -1)
                {
                    string hoTen = txtName.Text.Trim();
                    string MaCoSo = cbCoSo.SelectedValue.ToString();
                    using (Context dbContext = new Context())
                    {
                        // Truy vấn LINQ để lấy dữ liệu từ bảng NHAN_VIEN
                        var nhanVienData = from nv in dbContext.NHAN_VIEN
                                           where (nv.Hide == false) && nv.CoSo == MaCoSo &&  nv.HoTen.Contains(hoTen)
                                           select new
                                           {
                                               MSNV = nv.MSNV,
                                               HoTen = nv.HoTen,
                                               CoSo = nv.CoSo,
                                               TenCoSo = nv.CO_SO1.TenCoSo,
                                           };

                        // Gán dữ liệu cho DataGridView dgvNhanVien
                        foreach (var nv in nhanVienData)
                        {
                            dgvNhanVien.Rows.Add(nv.MSNV, nv.HoTen, nv.CoSo, nv.TenCoSo);
                        }
                    }
                }
                //Tìm theo Họ Tên, MSNV
                else if (!string.IsNullOrEmpty(txtMSNV.Text) && !string.IsNullOrEmpty(txtName.Text) && cbCoSo.SelectedIndex == -1)
                {
                    string Msnv = txtMSNV.Text.Trim();
                    string hoTen = txtName.Text.Trim();
                    using (Context dbContext = new Context())
                    {
                        // Truy vấn LINQ để lấy dữ liệu từ bảng NHAN_VIEN
                        var nhanVienData = from nv in dbContext.NHAN_VIEN
                                           where (nv.Hide == false) && nv.MSNV.Contains(Msnv) && nv.HoTen.Contains(hoTen)
                                           select new
                                           {
                                               MSNV = nv.MSNV,
                                               HoTen = nv.HoTen,
                                               CoSo = nv.CoSo,
                                               TenCoSo = nv.CO_SO1.TenCoSo,
                                           };

                        // Gán dữ liệu cho DataGridView dgvNhanVien
                        foreach (var nv in nhanVienData)
                        {
                            dgvNhanVien.Rows.Add(nv.MSNV, nv.HoTen, nv.CoSo, nv.TenCoSo);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(txtMSNV.Text) && !string.IsNullOrEmpty(txtName.Text) && cbCoSo.SelectedIndex != -1)
                {
                    string Msnv = txtMSNV.Text.Trim();
                    string hoTen = txtName.Text.Trim();
                    string MaCoSo = cbCoSo.SelectedValue.ToString();
                    using (Context dbContext = new Context())
                    {
                        // Truy vấn LINQ để lấy dữ liệu từ bảng NHAN_VIEN
                        var nhanVienData = from nv in dbContext.NHAN_VIEN
                                           where (nv.Hide == false) && nv.MSNV.Contains(Msnv) && nv.HoTen.Contains(hoTen) && nv.CoSo==MaCoSo
                                           select new
                                           {
                                               MSNV = nv.MSNV,
                                               HoTen = nv.HoTen,
                                               CoSo = nv.CoSo,
                                               TenCoSo = nv.CO_SO1.TenCoSo,
                                           };

                        // Gán dữ liệu cho DataGridView dgvNhanVien
                        foreach (var nv in nhanVienData)
                        {
                            dgvNhanVien.Rows.Add(nv.MSNV, nv.HoTen, nv.CoSo, nv.TenCoSo);
                        }
                    }
                }    
            }
        }

        private void LoadAutoComplete()
        {
            AutoCompleteStringCollection autoName = new AutoCompleteStringCollection();
            AutoCompleteStringCollection autoMSNV = new AutoCompleteStringCollection();
            try
            {
                using (Context dbContext = new Context())
                {
                    // Truy vấn LINQ để lấy dữ liệu từ bảng NHAN_VIEN
                    var lstNV = from nv in dbContext.NHAN_VIEN
                                       where (nv.Hide == false)
                                       select new
                                       {
                                           HoTen = nv.HoTen,
                                           MSNV = nv.MSNV,
                                           CoSo = nv.CoSo,
                                           TenCoSo = nv.CO_SO1.TenCoSo,
                                       };
                    foreach (var NV in lstNV)
                    {
                        autoName.Add(NV.HoTen);
                        autoMSNV.Add(NV.MSNV);
                    }    
                }
                txtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtName.AutoCompleteCustomSource = autoName;
                
                txtMSNV.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtMSNV.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtMSNV.AutoCompleteCustomSource = autoMSNV;

            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi chi tiết
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu trong LoadAutoComplete().\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMSNV_TextChanged(object sender, EventArgs e)
        {
            btnTimNV.Enabled = true;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            btnTimNV.Enabled = true;
        }

        private void cbCoSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnTimNV.Enabled = true;
        }

        private void btnRefreshNV_Click(object sender, EventArgs e)
        {

        }
    }
}
