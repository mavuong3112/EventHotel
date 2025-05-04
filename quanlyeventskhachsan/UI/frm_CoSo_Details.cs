using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
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
using quanlyeventskhachsan.MODEL;

namespace quanlyeventskhachsan.UI 
{
    public partial class frm_CoSo_Details : Form
    {
        public bool isCreate = true;
        public frm_CoSo_Details()
        {
            InitializeComponent();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if(isCreate)
            {
                AddNewCs();
            }
            else
            {
                EditCS();
            }
        }
        private void AddNewCs()
        {
            if (txtMaCoSo.Text.Length <= 0) { return; }
            try
            {
                using (Context db = new Context())
                {
                    CO_SO CS = new CO_SO();
                    CS.MaCoSo = txtMaCoSo.Text.Trim();
                    CS.Hide = false;
                    CS.TenCoSo = txtTenCoSo.Text.Trim();
                    CS.SDT = txtSdtCS.Text.Trim();
                    CS.Email = txtEmailCS.Text.Trim();
                    CS.NgayThanhLap = dtpCSBegin.Value;
                    db.CO_SOs.Add(CS);
                    MessageBox.Show("Thêm thành công");
                    db.SaveChanges();
                    MessageBox.Show("Thêm thành công");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi, xin hãy báo lại với admin \n\n***************************************** \n\n" + ex.Message.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditCS()
        {
            if (txtMaCoSo.Text.Length <= 0) { return; }
            try
            {
                using (Context db = new Context())
                {
                    CO_SO CS = db.CO_SOs.Find(txtMaCoSo.Text);
                    if (CS == null)
                        return;
                    CS.Hide = false;
                    CS.TenCoSo = txtTenCoSo.Text.Trim();
                    CS.SDT = txtSdtCS.Text.Trim();
                    CS.Email = txtEmailCS.Text.Trim();
                    CS.NgayThanhLap = dtpCSBegin.Value;
                    db.Entry(CS).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Sửa thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi, xin hãy báo lại với admin \n\n*****************************************\n\n " + ex.Message.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadFrmEditCoSo(string MaCoSo)
        {
            try
            {
                using (Context db = new Context())
                {
                    CO_SO CS = db.CO_SOs.Find(MaCoSo);
                    if (CS == null)
                        Close();
                    txtMaCoSo.Text = MaCoSo; txtMaCoSo.ReadOnly = true;
                    txtTenCoSo.Text = CS.TenCoSo;
                    txtSdtCS.Text = CS.SDT;
                    txtEmailCS.Text = CS.Email;
                    dtpCSBegin.Value = (CS.NgayThanhLap == null)? DateTime.Today: (DateTime)CS.NgayThanhLap;
                    btnManage.Text = "Sửa";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi, xin hãy báo lại với admin \n\n*****************************************\n\n " + ex.Message.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
                if (isCreate)
                {
                    AddNewCs();
                    LoadDataToDGV_CoSo();


                }
                else
                {
                    EditCS();
                }
        }


        private void LoadDataToDGV_CoSo()
        {
            try
            {
                using (Context dbContext = new Context())
                {
                    // Truy vấn LINQ để lấy dữ liệu từ bảng KHOA
                    var coSoData = from CoSo in dbContext.CO_SOs
                                   where CoSo.Hide == false
                                   select new
                                   {
                                       MaCoSo = CoSo.MaCoSo,
                                       TenCoSo = CoSo.TenCoSo,
                                       SDT = CoSo.SDT,
                                       Email = CoSo.Email,
                                       NgayThanhLap = CoSo.NgayThanhLap
                                   };

                    // Gán dữ liệu cho DataGridView dgvKhoa
                    dgvCoSo.DataSource = coSoData.ToList();

                    // Đổi tên tiêu đề của các cột
                    dgvCoSo.Columns["MaCoSo"].HeaderText = "Mã Cơ Sở";
                    dgvCoSo.Columns["TenCoSo"].HeaderText = "Tên Cơ Sở";
                    dgvCoSo.Columns["SDT"].HeaderText = "Số Điện Thoại";
                    dgvCoSo.Columns["Email"].HeaderText = "Email";
                    dgvCoSo.Columns["NgayThanhLap"].HeaderText = "Ngày Thành Lập";
                    dgvCoSo.Columns["NgayThanhLap"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi chi tiết
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu Cơ Sở .\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void frmCoSoDetails_Load(object sender, EventArgs e)
        {
            LoadDataToDGV_CoSo();
        }

        private void btn_Find(object sender, EventArgs e)
        {

        }

        private void btn_Import(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm",
                Title = "Select an Excel File"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        if (worksheet != null)
                        {
                            List<CO_SO> csList = new List<CO_SO>();

                            for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                            {
                                string maCoSo   = worksheet.Cells[row, 1].Value?.ToString();
                                string tenCoSo  = worksheet.Cells[row, 2].Value?.ToString();
                                string sdt      = worksheet.Cells[row, 3].Value?.ToString();
                                string email    = worksheet.Cells[row, 4].Value?.ToString();
                                var dayTemp     = worksheet.Cells[row, 5].Value.ToString();
                                DateTime ngayThanhLap = new DateTime();
                                if (dayTemp != null)
                                {
                                    ngayThanhLap = DateTime.ParseExact(dayTemp,"dd/MM/yyyy",null);
                                }
                                else ngayThanhLap = DateTime.Now;

                                if (!string.IsNullOrEmpty(maCoSo) && !string.IsNullOrEmpty(tenCoSo))
                                {
                                    csList.Add(new CO_SO
                                    {
                                        MaCoSo = maCoSo,
                                        TenCoSo = tenCoSo,
                                        SDT = sdt,
                                        Email = email,
                                        NgayThanhLap = ngayThanhLap
                                    });
                                }
                            }

                            // Check for existing records
                            List<string> existingMaCoSoList = CheckExistingMaCoSo(csList.Select(k => k.MaCoSo).ToList());

                            if (existingMaCoSoList.Any())
                            {
                                MessageBox.Show($"The following MaCoSo already exists: {string.Join(", ", existingMaCoSoList)}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Save the list to the database
                            SaveCoSoListToDatabase(csList);

                            // Refresh the DataGridView
                            LoadDataToDGV_CoSo();

                            MessageBox.Show("Import successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No data found in the Excel file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private List<string> CheckExistingMaCoSo(List<string> maCoSoList)
        {
            using (Context db = new Context())
            {
                // Filter the existing MaCoSo from the database
                List<string> existingMaMaCoSoList = db.CO_SOs.Where(k => maCoSoList.Contains(k.MaCoSo)).Select(k => k.MaCoSo).ToList();
                return existingMaMaCoSoList;
            }
        }


        private void SaveCoSoListToDatabase(List<CO_SO> coSoList)
        {
            try
            {
                using (Context db = new Context())
                {
                    foreach (var coSo in coSoList)
                    {
                        CO_SO existingCoSo = db.CO_SOs.Find(coSo.MaCoSo);

                        if (existingCoSo == null)
                        {
                            // Add new KHOA
                            coSo.Hide = false;
                            db.CO_SOs.Add(coSo);
                        }
                        else
                        {
                            // Update existing KHOA
                            existingCoSo.TenCoSo = coSo.TenCoSo;
                            existingCoSo.SDT = coSo.SDT;
                            existingCoSo.Email = coSo.Email;
                            existingCoSo.NgayThanhLap = coSo.NgayThanhLap;
                            existingCoSo.Hide = false;
                        }
                    }

                    // Save changes to the database
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving data to the database: {ex.Message}");
            }
        }

        private void btn_Export(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "Export to Excel"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("CoSoData");

                        // Headers
                        worksheet.Cells[1, 1].Value = "MaCoSo";
                        worksheet.Cells[1, 2].Value = "TenCoSo";
                        worksheet.Cells[1, 3].Value = "SDT";
                        worksheet.Cells[1, 4].Value = "Email";
                        worksheet.Cells[1, 5].Value = "NgayThanhLap";

                        // Data
                        int row = 2;
                        foreach (DataGridViewRow dgvRow in dgvCoSo.Rows)
                        {
                            DateTime NTL = (DateTime)dgvRow.Cells["NgayThanhLap"].Value;
                            worksheet.Cells[row, 1].Value = dgvRow.Cells["MaCoSo"].Value?.ToString();
                            worksheet.Cells[row, 2].Value = dgvRow.Cells["TenCoSo"].Value?.ToString();
                            worksheet.Cells[row, 3].Value = dgvRow.Cells["SDT"].Value?.ToString();
                            worksheet.Cells[row, 4].Value = dgvRow.Cells["Email"].Value?.ToString();
                            worksheet.Cells[row, 5].Value = NTL.ToString("dd/MM/yyyy");

                            row++;
                        }

                        // Auto-fit columns
                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                        // Save the Excel file
                        package.Save();

                        MessageBox.Show("Export successful.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvCoSo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedCoSo = dgvCoSo.SelectedRows[0];
            if (selectedCoSo != null)
            {
                string Ma = selectedCoSo.Cells["MaCoSo"].Value?.ToString();
                string Ten = selectedCoSo.Cells["TenCoSo"].Value?.ToString();
                string sdt = selectedCoSo.Cells["SDT"].Value?.ToString();
                string email = selectedCoSo.Cells["Email"].Value?.ToString();
                dtpCSBegin.Text = selectedCoSo.Cells["NgayThanhLap"].Value.ToString();

                txtMaCoSo.Text = Ma;
                txtTenCoSo.Text = Ten;
                txtSdtCS.Text = sdt;
                txtEmailCS.Text = email;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            EditCS();
            LoadDataToDGV_CoSo();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvCoSo.SelectedRows.Count > 0)
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa CoSo {dgvCoSo.SelectedRows[0].Cells["TenCoSo"].Value.ToString()} không?", "Xác nhận xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    if (txtMaCoSo.Text.Length <= 0) { return; }
                    try
                    {
                        using (Context db = new Context())
                        {
                            CO_SO CS = db.CO_SOs.Find(txtMaCoSo.Text);
                            if (CS == null)
                                return;
                            CS.Hide = true;
                            db.Entry(CS).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            LoadDataToDGV_CoSo();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Đã xảy ra lỗi, xin hãy báo lại với admin \n\n*****************************************\n\n " + ex.Message.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    return;
            }
        }
        private void txtSdtK_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }
    }
}
