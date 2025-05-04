
using OfficeOpenXml;
using quanlyeventskhachsan.MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace quanlyeventskhachsan.UI
{
    public partial class frm_DoiTac_Details : Form
    {
        Int32 id;
        Context db = new Context();
        DOI_TAC DOI_TAC = new DOI_TAC();

        public frm_DoiTac_Details()
        {
            InitializeComponent();
            Load();
            AddBinding();
        }

        private void AddBinding()
        {
            txtName.DataBindings.Add(new Binding("Text", guna2DataGridView1.DataSource, "a"));
            txtRep.DataBindings.Add(new Binding("Text", guna2DataGridView1.DataSource, "b"));
            txtPhone.DataBindings.Add(new Binding("Text", guna2DataGridView1.DataSource, "d"));
            txtMail.DataBindings.Add(new Binding("Text", guna2DataGridView1.DataSource, "c"));
        }

        private new void Load()
        {
            var result = from dt in db.DOI_TAC
                         where dt.Hide == false
                         select new
                         {
                             a = dt.TenDoiTac,
                             b = dt.DaiDien,
                             c = dt.Email,
                             d = dt.SDT,
                             e = dt.ID_DoiTac
                         };

            guna2DataGridView1.DataSource = result.ToList();

            guna2DataGridView1.Columns["a"].HeaderText = "Tên Đối Tác";
            guna2DataGridView1.Columns["b"].HeaderText = "Người Đại Diện";
            guna2DataGridView1.Columns["c"].HeaderText = "Địa chỉ Email";
            guna2DataGridView1.Columns["d"].HeaderText = "Số điện thoại";
            guna2DataGridView1.Columns["e"].Visible = false;
        }

        private void reset()
        {
            txtName.Text = txtRep.Text = txtPhone.Text = txtMail.Text = "";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string tenDoiTac = txtName.Text.Trim();
            string daiDien = txtRep.Text.Trim();
            string email = txtMail.Text.Trim();
            string sdt = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(tenDoiTac) || string.IsNullOrEmpty(daiDien) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (Context dbContext = new Context())
            {
                if (dbContext.DOI_TAC.Any(dt => dt.TenDoiTac == tenDoiTac))
                {
                    MessageBox.Show("Đối tác đã tồn tại.", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DOI_TAC newDoiTac = new DOI_TAC
                {
                    TenDoiTac = tenDoiTac,
                    DaiDien = daiDien,
                    Email = email,
                    SDT = sdt,
                    Hide = false
                };

                dbContext.DOI_TAC.Add(newDoiTac);
                dbContext.SaveChanges();

                MessageBox.Show("Thêm đối tác thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reset();
                Load();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow == null)
                return;

            id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["e"].Value);
            DOI_TAC = db.DOI_TAC.Find(id);

            DOI_TAC.TenDoiTac = txtName.Text.Trim();
            DOI_TAC.DaiDien = txtRep.Text.Trim();
            DOI_TAC.SDT = txtPhone.Text.Trim();
            DOI_TAC.Email = txtMail.Text.Trim();

            db.Entry(DOI_TAC).State = EntityState.Modified;
            db.SaveChanges();
            Load();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa Đối tác {guna2DataGridView1.SelectedRows[0].Cells["a"].Value.ToString()} không?", "Xác nhận xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)

                //if (MessageBox.Show($"Bạn có chắc chắn muốn xóa Đối tác {tenDoiTac} không?", "Xác nhận xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["e"].Value);
                    DOI_TAC = db.DOI_TAC.Find(id);
                    DOI_TAC.Hide = true;

                    db.Entry(DOI_TAC).State = EntityState.Modified;
                    db.SaveChanges();
                    Load();
                }
                    return;
            }
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Find(object sender, EventArgs e)
        {
            FindDoiTac();
        }

        private void FindDoiTac()
        {
            string searchKeyword = txtName.Text.Trim();

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                var result = from dt in db.DOI_TAC
                             where dt.TenDoiTac.Contains(searchKeyword) && dt.Hide == false
                             select new
                             {
                                 a = dt.TenDoiTac,
                                 b = dt.DaiDien,
                                 c = dt.Email,
                                 d = dt.SDT,
                                 e = dt.ID_DoiTac
                             };

                guna2DataGridView1.DataSource = result.ToList();
            }
            else
            {
                // Nếu từ khóa tìm kiếm rỗng, load lại toàn bộ dữ liệu
                Load();
            }
        }

        private void btn_Import(object sender, EventArgs e)
        {
            // Gọi hàm import
            ImportDoiTac();
        }

      

        private void btn_Export(object sender, EventArgs e)
        {
            // Gọi hàm export
            ExportDoiTac();
        }

        private void ExportDoiTac()
        {
            try
            {
                // Tạo một đối tượng ExcelPackage
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    // Tạo một Sheet có tên là "DoiTac"
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("DoiTac");

                    // Ghi dữ liệu từ DataGridView vào Excel
                    for (int i = 1; i <= guna2DataGridView1.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i].Value = guna2DataGridView1.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Style.Font.Bold = true;
                    }

                    for (int i = 1; i <= guna2DataGridView1.Rows.Count; i++)
                    {
                        for (int j = 1; j <= guna2DataGridView1.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 1, j].Value = guna2DataGridView1.Rows[i - 1].Cells[j - 1].Value;
                        }
                    }
                    worksheet.Cells.AutoFitColumns();
                    // Lưu file Excel
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                        saveFileDialog.FilterIndex = 2;
                        saveFileDialog.RestoreDirectory = true;

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                            excelPackage.SaveAs(excelFile);
                            MessageBox.Show("Export thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình export. Chi tiết lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportDoiTac()
        {
            try
        {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo excelFile = new FileInfo(openFileDialog.FileName);

                        using (ExcelPackage excelPackage = new ExcelPackage(excelFile))
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();

                            if (worksheet != null)
                            {
                                // Xóa dữ liệu hiện tại trong DataGridView
                                guna2DataGridView1.DataSource = null;

                                // Đọc dữ liệu từ Excel và gán cho DataGridView
                                var data = worksheet.Cells["A1:" + worksheet.Dimension.End.Address].Value;
                                guna2DataGridView1.DataSource = data;

                                MessageBox.Show("Import thành công!");
                            }
                            else
                            {
                                MessageBox.Show("File Excel không có dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình import. Chi tiết lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];

                // Lấy giá trị từ cột tương ứng trong dòng được chọn
                string TenDT = selectedRow.Cells["a"].Value.ToString();
                string Daidien = selectedRow.Cells["b"].Value.ToString();
                string email = selectedRow.Cells["c"].Value.ToString();
                string sdt = selectedRow.Cells["d"].Value.ToString();

                // Hiển thị thông tin lên các TextBox
                txtName.Text = TenDT;
                txtRep.Text = Daidien;
                txtMail.Text = email;
                txtPhone.Text = sdt;
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }
    }
}
