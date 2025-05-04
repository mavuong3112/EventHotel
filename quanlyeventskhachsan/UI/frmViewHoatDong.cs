using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Collections;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Xml.Serialization;
using ComponentFactory.Krypton.Toolkit;
using OfficeOpenXml;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using quanlyeventskhachsan.MODEL;
namespace quanlyeventskhachsan.UI
{
    public partial class frmViewHoatDong : Form
    {
        public frmViewHoatDong()
        {
            InitializeComponent();
            this.Text = "Tạo Mới Hoạt Động" ;
        }
        public void LoadFormView(int idHD)
        {
            try
            {
                using (Context db = new Context())
                {
                    HOAT_DONG hD = db.HOAT_DONG.Find(idHD);
                    //txtMaHD.Text = hD.MaHD.Trim();
                    txtTenHD.Text = hD.TenHoatDong;
                    txtLoai.Text = hD.Loai == null ? "" : hD.Loai;
                    txtDateBegin.Text = hD.NgayBatDau == null ? DateTime.Now.ToString() : hD.NgayBatDau.ToString();
                    txtDateEnd.Text = hD.NgayKetThuc == null ? DateTime.Now.ToString() : hD.NgayKetThuc.ToString();
                    LoadHD_NhanVien(hD);
                    LoadHD_QL(hD);
                    LoadHD_DT(hD);
                    LoadHD_TT(hD);
                    LoadHD_TC(hD);
                    foreach (Control g in panel1.Controls)
                    {
                        if (g is GroupBox)
                        {
                            foreach (Control c in g.Controls)
                            {
                                if (c is KryptonNumericUpDown)
                                {
                                    KryptonNumericUpDown k = (KryptonNumericUpDown)c;
                                    k.ReadOnly = true;
                                    k.Increment = 0;
                                }    
                                else
                                    continue;
                            }
                        }
                        else
                            continue;
                    }
                    lblQL_TotalNumber.Text = dgv_QL.Rows.Count.ToString();
                    lblNV_TotalNumber.Text = dgvNhanVien.Rows.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading form:\n" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LoadHD_NhanVien(HOAT_DONG hD)
        {
            List<HD_NHAN_VIEN> NVList = hD.HD_NHAN_VIEN.ToList();
            foreach (HD_NHAN_VIEN NV in NVList)
            {
                DataGridViewRow row = new DataGridViewRow();
                //dgvNhanVien.Rows.Add(NV.MSNV, NV.NHAN_VIEN.HoTen, NV.NHAN_VIEN.CO_SO1.TenCoSo, NV.VaiTro, NV.GhiChu, NV.NHAN_VIEN.CoSo);
            }
        }
        
        private void LoadHD_QL(HOAT_DONG hD)
        {    //MaQL,HoTenLot,Ten,GVKhoa,GV_DonVi,GVKHoa_DB
            List<HD_QUANLY> List = hD.HD_QUANLY.ToList();
            foreach (HD_QUANLY QL in List)
            {
                if (QL.QUAN_LY == null || QL.QUAN_LY.CoSo == null || QL.QUAN_LY.CO_SO1.Hide == true || QL.VaiTro ==null  ){ continue; }

                DataGridViewRow row = new DataGridViewRow();
                dgv_QL.Rows.Add(QL.MaQL, QL.QUAN_LY.HoTenLot, QL.QUAN_LY.Ten, QL.QUAN_LY.CO_SO1.TenCoSo, QL.VaiTro, QL.QUAN_LY.CoSo);
            }
        }
        private void LoadHD_DT(HOAT_DONG hD)
        {    //DT_Ten,DT_DaiDien,DT_SDT,DT_Email,DT_NoiDung,ID_DB
            List<HD_DOITAC> List = hD.HD_DOITAC.ToList();
            foreach (HD_DOITAC DT in List)
            {
                dgvDoiTac.Rows.Add(DT.DOI_TAC.TenDoiTac, DT.DOI_TAC.DaiDien, DT.DOI_TAC.SDT, DT.DOI_TAC.Email, DT.NoiDung, DT.DOI_TAC.ID_DoiTac);
            }
        }

        private void LoadHD_TT(HOAT_DONG hD)
        {    //TT_Name, TT_Rep, TT_SDT, TT_Email, TT_Notes, TT_IDDB
            List<HD_TAITRO> List = hD.HD_TAITRO.ToList();
            foreach (HD_TAITRO TT in List)
            {
                dgvTaiTro.Rows.Add(TT.TAI_TRO.TenTaiTro, TT.TAI_TRO.DaiDien, TT.TAI_TRO.SDT, TT.TAI_TRO.Email, TT.NoiDung, TT.TAI_TRO.ID_TaiTro);
            }
        }

        private void LoadHD_TC(HOAT_DONG hD) //Lấy thông tin tài chính mới nhất
        {
            TAI_CHINH Latest = hD.TAI_CHINH.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            if (Latest == null)
                return;
            txtTC_TieuDe.Text = Latest.TieuDe;
            txtTC_Khac.Text = Latest.Khac;
            numUEF.Value = Latest.UEF == null? 0 : (decimal)Latest.UEF;
            numTaiTro.Value = Latest.TaiTro == null ? 0 : (decimal)Latest.TaiTro;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNVExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);

                        using (ExcelPackage excelPackage = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(txtTenHD.Text);

                            // Ghi header của DataGridView vào Excel
                            for (int i = 1; i <= dgvNhanVien.Columns.Count; i++)
                            {
                                worksheet.Cells[3, i].Value = dgvNhanVien.Columns[i - 1].HeaderText;
                                worksheet.Cells[3, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgvNhanVien.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgvNhanVien.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 3, j].Value = dgvNhanVien.Rows[i - 1].Cells[j - 1].Value;
                                }
                            }
                            worksheet.Cells.AutoFitColumns(0);
                            excelPackage.SaveAs(excelFile);
                            MessageBox.Show("Export thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình export. Chi tiết lỗi:\n\n " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQLExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        using (ExcelPackage excelPackage = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(txtTenHD.Text);

                            // Ghi header của DataGridView vào Excel
                            for (int i = 1; i <= dgv_QL.Columns.Count; i++)
                            {
                                worksheet.Cells[3, i].Value = dgv_QL.Columns[i - 1].HeaderText;
                                worksheet.Cells[3, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgv_QL.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgv_QL.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 3, j].Value = dgv_QL.Rows[i - 1].Cells[j - 1].Value;
                                }
                            }
                            worksheet.Cells.AutoFitColumns(0);
                            excelPackage.SaveAs(excelFile);
                            MessageBox.Show("Export thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình export. Chi tiết lỗi:\n\n " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDTExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        using (ExcelPackage excelPackage = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(txtTenHD.Text);

                            // Ghi header của DataGridView vào Excel
                            for (int i = 1; i <= dgvDoiTac.Columns.Count; i++)
                            {
                                worksheet.Cells[3, i].Value = dgvDoiTac.Columns[i - 1].HeaderText;
                                worksheet.Cells[3, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgvDoiTac.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgvDoiTac.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 3, j].Value = dgvDoiTac.Rows[i - 1].Cells[j - 1].Value;
                                }
                            }
                            worksheet.Cells.AutoFitColumns(0);
                            excelPackage.SaveAs(excelFile);
                            MessageBox.Show("Export thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình export. Chi tiết lỗi:\n\n " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTTExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        using (ExcelPackage excelPackage = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(txtTenHD.Text);

                            // Ghi header của DataGridView vào Excel
                            for (int i = 1; i <= dgvTaiTro.Columns.Count; i++)
                            {
                                worksheet.Cells[3, i].Value = dgvTaiTro.Columns[i - 1].HeaderText;
                                worksheet.Cells[3, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgvTaiTro.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgvTaiTro.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 3, j].Value = dgvTaiTro.Rows[i - 1].Cells[j - 1].Value;
                                }
                            }
                            worksheet.Cells.AutoFitColumns(0);
                            excelPackage.SaveAs(excelFile);
                            MessageBox.Show("Export thành công!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình export. Chi tiết lỗi:\n\n " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmViewHoatDong_Load(object sender, EventArgs e)
        {

        }
    }
}
