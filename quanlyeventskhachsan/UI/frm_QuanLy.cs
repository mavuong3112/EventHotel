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
using System.Windows.Media;
using quanlyeventskhachsan.MODEL;

namespace quanlyeventskhachsan.UI 
{
    public partial class frm_QuanLy : Form
    {
        public frm_QuanLy()
        {
            InitializeComponent();
        }

        private void frm_QuanLy_Load(object sender, EventArgs e)
        {      
            // Lấy dữ liệu từ bảng GIANG_VIEN và hiển thị trong dgv_GiangVien
            LoadDataToDGV_QuanLy();
            LoadDataToCbCS();
        }
        private void LoadDataToCbCS()
        {
            try
            {
                using (Context db = new Context())
                {
                    var coSo = from cs in db.CO_SOs
                               where (cs.Hide == false)
                               select new
                               {
                                   MaCS = cs.MaCoSo,
                                   Ten = cs.TenCoSo,
                               };
                    cbCS.DataSource = coSo.ToList();
                    cbCS.DisplayMember = "Ten";
                    cbCS.ValueMember = "MaCS";
                    cbCS.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Cơ Sở:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddDataToQuanLy(string maQuanLy, string hoTenLot, string ten, string coSo)
        {
            using (Context db = new Context())
            {
                // Kiểm tra xem quản lý đã tồn tại chưa
                QUAN_LY existingQL = db.QUAN_LY.FirstOrDefault(ql => ql.MaQL == maQuanLy);

                if (existingQL == null)
                {
                    // Nếu chưa tồn tại, thêm giảng viên mới vào cơ sở dữ liệu
                    QUAN_LY newQL = new QUAN_LY
                    {
                        MaQL = maQuanLy,
                        HoTenLot = hoTenLot,
                        Ten = ten,
                        CoSo = coSo
                    };

                    db.QUAN_LY.Add(newQL);
                    db.SaveChanges();
                }
            }
        }

        private void LoadDataToDGV_QuanLy()
        {
            dgv_QuanLy.Rows.Clear();
            dgv_QuanLy.Refresh();
            try
            {
                using (Context db = new Context())
                {
                    // Truy vấn LINQ để lấy dữ liệu từ bảng GIANG_VIEN
                    var QLData = (from ql in db.QUAN_LY
                                         where ql.Hide == false
                                        select new
                                        {
                                            MaQL = ql.MaQL,
                                            HoTenLot = ql.HoTenLot,
                                            Ten = ql.Ten,
                                            CoSo = ql.CoSo,
                                            TenCoSo = ql.CO_SO1.TenCoSo,
                                        }).Take(500);

                    // Gán dữ liệu cho DataGridView dgv_GiangVien
                    foreach (var ql in QLData)
                    {
                        dgv_QuanLy.Rows.Add(ql.MaQL,ql.HoTenLot,ql.Ten, ql.CoSo, ql.TenCoSo);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi chi tiết
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu Quản Lý.\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reset()
        {
            txtHoTenLot.Text = txtMaQL.Text = txtName.Text = "";
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các điều khiển
            string maQL = txtMaQL.Text.Trim();
            string hoTenLot = txtHoTenLot.Text.Trim();
            string ten = txtName.Text.Trim();
            string coSo = cbCS.SelectedValue.ToString().Trim();

            // Kiểm tra xem MaQL có tồn tại không
            using (Context dbContext = new Context())
            {
                QUAN_LY existingQL = dbContext.QUAN_LY.Find(maQL);

                if (existingQL == null)
                {
                    // Nếu MaQL chưa tồn tại, thêm mới
                    QUAN_LY newQL = new QUAN_LY
                    {
                        MaQL = maQL,
                        HoTenLot = hoTenLot,
                        Ten = ten,
                        CoSo = coSo,
                        Hide = false
                    };

                    dbContext.QUAN_LY.Add(newQL);
                    dbContext.SaveChanges();

                    MessageBox.Show("Thêm quản lý thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reset();
                    LoadDataToDGV_QuanLy();
                }
                else
                    MessageBox.Show("Mã quản lý đã tồn tại.", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_QuanLy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgv_QuanLy.Rows[e.RowIndex];

                // Lấy giá trị từ cột tương ứng trong dòng được chọn
                string maQL = selectedRow.Cells["MaQL"].Value.ToString();
                string hoTenLot = selectedRow.Cells["HoTenLot"].Value.ToString();
                string ten = selectedRow.Cells["Ten"].Value.ToString();
                string coSo = selectedRow.Cells["MaCoSo"].Value.ToString();

                // Hiển thị thông tin lên các TextBox
                txtMaQL.Text = maQL;
                txtHoTenLot.Text = hoTenLot;
                txtName.Text = ten;
                cbCS.SelectedValue = coSo;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ các TextBox
            string maQL = txtMaQL.Text.Trim();
            string hoTenLot = txtHoTenLot.Text.Trim();
            string ten = txtName.Text.Trim();
            string coSo = cbCS.SelectedValue.ToString().Trim();

            using (Context dbContext = new Context())
            {
                // Kiểm tra xem giáo viên có tồn tại không
                QUAN_LY existingQL = dbContext.QUAN_LY.Find(maQL);

                if (existingQL != null)
                {
                    // Cập nhật thông tin quản lý
                    existingQL.HoTenLot = hoTenLot;
                    existingQL.Ten = ten;
                    existingQL.CoSo = coSo;
                    existingQL.Hide = false;

                    dbContext.SaveChanges();

                    MessageBox.Show("Cập nhật thông tin quản lý thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại DataGridView sau khi cập nhật thông tin quản lý
                    reset();
                    LoadDataToDGV_QuanLy();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy quản lý có MaQL là " + maQL, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
                // Kiểm tra xem có dòng được chọn trong dgv_QuanLy không
            if (dgv_QuanLy.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Bạn có muốn xóa quản lý " + dgv_QuanLy.SelectedRows[0].Cells["Ten"].Value.ToString() + " - Mã " + dgv_QuanLy.SelectedRows[0].Cells["MaQL"].Value.ToString() 
                    + " không?", "Xác nhận xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    // Lấy MaQL của quản lý được chọn
                    string maQL = dgv_QuanLy.SelectedRows[0].Cells["MaQL"].Value.ToString();

                    // Thực hiện xóa quản lý trong cơ sở dữ liệu
                    using (Context dbContext = new Context())
                    {
                        // Lấy quản lý cần xóa
                        QUAN_LY existingQL = dbContext.QUAN_LY.Find(maQL);

                        if (existingQL != null)
                        {
                            // Xóa quản lý từ DbSet
                            existingQL.Hide = true;
                            dbContext.Entry(existingQL).State = System.Data.Entity.EntityState.Modified;
                            // Lưu thay đổi vào cơ sở dữ liệu
                            dbContext.SaveChanges();

                            MessageBox.Show("Xóa quản lý thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Cập nhật lại DataGridView sau khi xóa quản lý
                            LoadDataToDGV_QuanLy();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy quản lý để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                    return;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn quản lý cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btn_importQL(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx;*.xlsm",
                Title = "Select an Excel File"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog1.FileName);

                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        if (worksheet != null)
                        {
                            // Bắt đầu từ dòng thứ 2 (dòng đầu tiên là tiêu đề)
                            for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                            {
                                // Lấy dữ liệu từ cột tương ứng
                                string maQL = worksheet.Cells[row, 1].Value?.ToString().Trim();
                                string hoTenLot = worksheet.Cells[row, 2].Value?.ToString().Trim();
                                string ten = worksheet.Cells[row, 3].Value?.ToString().Trim();
                                string tenCoSo = worksheet.Cells[row, 4].Value?.ToString().Trim();

                                // Kiểm tra dữ liệu không rỗng
                                if (!string.IsNullOrEmpty(maQL) && !string.IsNullOrEmpty(hoTenLot) && !string.IsNullOrEmpty(ten))
                                {
                                    // Thêm quản lý vào cơ sở dữ liệu
                                    AddQuanLyToDatabase(maQL, hoTenLot, ten, tenCoSo);
                                }
                            }

                            MessageBox.Show("Thêm quản lý từ Excel thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Cập nhật lại DataGridView sau khi thêm quản lý
                            LoadDataToDGV_QuanLy();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu trong tệp Excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddQuanLyToDatabase(string maQL, string hoTenLot, string ten, string tenCoSo)
        {
            using (Context dbContext = new Context())
            {
                // Kiểm tra xem coSo đã tồn tại hay không
                CO_SO existingCoSo = dbContext.CO_SOs.FirstOrDefault(k => k.MaCoSo == tenCoSo);

                if (existingCoSo == null)
                {
                    // Nếu coSo không tồn tại, có thể thông báo lỗi hoặc thêm mới coSo
                    MessageBox.Show($"Cơ sở '{tenCoSo}' không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Hoặc thêm mã lỗi/xử lý tương ứng tùy vào yêu cầu của bạn
                }

                // Kiểm tra xem mã quản lý đã tồn tại hay chưa
                bool maQLExists = dbContext.QUAN_LY.Any(ql => ql.MaQL == maQL);

                if (maQLExists)
                {
                    // Nếu mã quản lý đã tồn tại, có thể thông báo lỗi hoặc thực hiện hành động tương ứng
                    MessageBox.Show($"Mã quản lý '{maQL}' đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Hoặc thêm mã lỗi/xử lý tương ứng tùy vào yêu cầu của bạn
                }

                // Tạo một đối tượng quản lý mới
                QUAN_LY newQuanLy = new QUAN_LY
                {
                    MaQL = maQL,
                    HoTenLot = hoTenLot,
                    Ten = ten,
                    CoSo = existingCoSo.MaCoSo
                };

                // Thêm quản lý mới vào cơ sở dữ liệu
                dbContext.QUAN_LY.Add(newQuanLy);

                // Lưu thay đổi vào cơ sở dữ liệu
                dbContext.SaveChanges();
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
                            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                            // Add a new worksheet to the Excel package
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("QuanLiData");

                            // Write headers to the worksheet
                            for (int col = 1; col <= dgv_QuanLy.Columns.Count; col++)
                            {
                                worksheet.Cells[1, col].Value = dgv_QuanLy.Columns[col - 1].HeaderText;
                            }

                            // Write data from DataGridView to the worksheet
                            for (int row = 0; row < dgv_QuanLy.Rows.Count; row++)
                            {
                                for (int col = 0; col < dgv_QuanLy.Columns.Count; col++)
                                {
                                    worksheet.Cells[row + 2, col + 1].Value = dgv_QuanLy.Rows[row].Cells[col].Value;
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
            // Check if the search keyword is empty
            if (string.IsNullOrEmpty(txtHoTenLot.Text.Trim()) && string.IsNullOrEmpty(txtMaQL.Text.Trim()) && string.IsNullOrEmpty(txtName.Text.Trim()) && cbCS.SelectedIndex == -1)
            {
                MessageBox.Show("Please enter a search keyword.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                dgv_QuanLy.Rows.Clear();
                dgv_QuanLy.Refresh();
                using (Context db = new Context())
                {
                    // Truy vấn LINQ để lấy dữ liệu từ bảng quản lý (Dữ liệu đầu tiên)
                    var quanLyData = (from ql in db.QUAN_LY
                                         where ql.Hide == false
                                         select new
                                         {
                                             MaQL = ql.MaQL,
                                             HoTenLot = ql.HoTenLot,
                                             Ten = ql.Ten,
                                             CoSo = ql.CoSo,
                                             TenCoSo = ql.CO_SO1.TenCoSo,
                                         });
                    //lọc dần qua từng điều kiện
                    if (!string.IsNullOrEmpty(txtMaQL.Text.Trim()))
                    {
                        string maql = txtMaQL.Text.Trim();
                        quanLyData = (from ql in quanLyData
                                         where ql.MaQL.Contains(maql)  //thêm && .Hide == false ở đây(và ở những cái dưới) thì sẽ không cần truy vấn đầu nữa (Dự phòng sau này dữ liệu lớn quá)
                                         select ql);
                    }
                    if (!string.IsNullOrEmpty(txtHoTenLot.Text.Trim()))
                    {
                        string tenlot = txtHoTenLot.Text.Trim();
                        quanLyData = (from ql in quanLyData
                                         where ql.HoTenLot.Contains(tenlot)
                                         select ql);
                    }
                    if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                    {
                        string ten = txtName.Text.Trim();
                        quanLyData = (from ql in quanLyData
                                         where ql.Ten.Contains(ten)
                                         select ql);
                    }
                    if (cbCS.SelectedIndex != -1)
                    {
                        string macoso = cbCS.SelectedValue.ToString();
                        quanLyData = (from ql in quanLyData
                                         where ql.CoSo == macoso
                                         select ql);
                    }    
                    // Gán dữ liệu cho DataGridView dgv_QuanLy
                    foreach (var ql in quanLyData)
                    {
                        dgv_QuanLy.Rows.Add(ql.MaQL, ql.HoTenLot, ql.Ten, ql.CoSo, ql.TenCoSo);
                    }
                }

            }
            
        }

        private void btnRefreshQL_Click(object sender, EventArgs e)
        {
            txtHoTenLot.Text = txtMaQL.Text = txtName.Text = "";
            LoadDataToCbCS();
            LoadDataToDGV_QuanLy();
        }
    }
}
