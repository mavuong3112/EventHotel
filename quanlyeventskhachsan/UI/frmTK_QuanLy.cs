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
    public partial class frmTK_QuanLy : Form
    {
        Context db = new Context();
        public frmTK_QuanLy()
        {
            InitializeComponent();
        }
        public void DisplayCMBCoSo(ComboBox a)
        {
            var kh = db.CO_SOs.Where(s=>s.Hide == false).Select(s => s);
            a.DataSource = kh.ToList();
            a.ValueMember = "MaCoSo";
            a.DisplayMember = "TenCoSo";
            a.SelectedIndex = -1;

        }
        private void frmTK_QuanLy_Load(object sender, EventArgs e)
        {   
            cmbCoSo.SelectedIndex = -1;
            DisplayCMBCoSo(cmbCoSo);    
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            ThongKeQuanLy();
            Xoa();
            btnLoc.Enabled = false;
        }
        private void ThongKeQuanLy()
        {
            try
            {
                dgvQL.Rows.Clear();
                dgvQL.Refresh();
                this.dgvQL.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                List<string> lstMaQL = new List<string>();
                List<string> lstTenQL = new List<string>();
                List<string> lstHoTenLotQL = new List<string>();
                lstMaQL = db.QUAN_LY.Where(x=>x.Hide == false).Select(x => x.MaQL).Take(200).ToList();
                lstTenQL = db.QUAN_LY.Where(x => x.Hide == false).Select(x => x.Ten).Take(200).ToList();
                lstHoTenLotQL = db.QUAN_LY.Where(x => x.Hide == false).Select(x => x.HoTenLot).Take(200).ToList();
                for (int j = 0; j < lstMaQL.Count; j++)
                {
                    string MaQL = lstMaQL[j];
                    string tenQL = lstTenQL[j];
                    string HoTenLotQL = lstHoTenLotQL[j];
                    dgvQL.Rows.Add();
                    dgvQL.Rows[j].Cells[0].Value = j + 1;
                    dgvQL.Rows[j].Cells[1].Value = MaQL;
                    dgvQL.Rows[j].Cells[2].Value = HoTenLotQL;
                    dgvQL.Rows[j].Cells[3].Value = tenQL;
                    List<string> coSo = (from s in db.QUAN_LY
                                         where s.MaQL == MaQL && s.Hide == false
                                         select (s.CO_SO1.TenCoSo)).ToList();
                    dgvQL.Rows[j].Cells[4].Value = coSo[0];
                    List<int> lstMaHD = new List<int>();
                    lstMaHD = (from s in db.QUAN_LY
                               join b in db.HD_QUANLY on s.MaQL equals b.MaQL
                               where s.MaQL == MaQL && b.HOAT_DONG.Hide == false
                               select (b.MaHD)).ToList();
                    if (lstMaHD.Count == 0) dgvQL.Rows[j].Cells[5].Value = " ";
                    else
                    {
                        string TenHD = "- ";
                        int MaHD = lstMaHD[0];
                        List<string> NameHD = (from s in db.HOAT_DONG
                                               where s.MaHD == MaHD && s.Hide == false
                                               select (s.TenHoatDong)).ToList();
                        TenHD = TenHD + NameHD[0];
                        for (int i = 1; i < lstMaHD.Count; i++)
                        {
                            MaHD = lstMaHD[i];
                            List<string> TenHoat = (from s in db.HOAT_DONG
                                                    where s.MaHD == MaHD && s.Hide == false 
                                                    select (s.TenHoatDong)).ToList();
                            TenHD = TenHD + "\n- " + TenHoat[0];
                        }
                        dgvQL.Rows[j].Cells[5].Value = TenHD;
                    }
                }
                Xoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        private void Xoa()
        {
            int n = dgvQL.Rows.Count;
            //MessageBox.Show(n.ToString());
            for (int i = 0; i < n; i++)
            {
                if (dgvQL.Rows[i].Cells[5].Value == " " || dgvQL.Rows[i].Cells[4].Value == " ")
                {
                    //Object stt = dgvNV.Rows[i].Cells[0].Value;
                    dgvQL.Rows.RemoveAt(dgvQL.Rows[i].Index);
                    i--;
                    n--;
                    // dgvNV.Rows[i+1].Cells[0].Value = stt;
                }
            }
            for (int i = 0; i < n - 1; i++)
            {
                dgvQL.Rows[i].Cells[0].Value = i + 1;
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*" })
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToExcel(dgvQL, sfd.FileName);
                }
        }
        private void ToExcel(DataGridView dtg, string fileName)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            try
            {
                //Tạo đối tượng COM.
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                //tạo mới một Workbooks bằng phương thức add()
                workbook = excel.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                //đặt tên cho sheet
                worksheet.Name = "Thống kê nhân viên";

                // export header trong DataGridView
                for (int i = 0; i < dtg.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dtg.Columns[i].HeaderText;
                }
                // export nội dung trong DataGridView
                for (int i = 0; i < dtg.RowCount; i++)
                {
                    for (int j = 0; j < dtg.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dtg.Rows[i].Cells[j].Value;
                    }
                }
                excel.Columns.AutoFit();
                // sử dụng phương thức SaveAs() để lưu workbook với filename
                workbook.SaveAs(fileName);
                //đóng workbook
                workbook.Close();
                excel.Quit();
                MessageBox.Show("Xuất dữ liệu ra Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workbook = null;
                worksheet = null;
            }
        }
            private void dtpBD_ValueChanged(object sender, EventArgs e)
        {
            dtpBD.CustomFormat = "yyyy-MM-dd";
            btnLoc.Enabled = true;
        }

        private void dtpBD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                dtpBD.CustomFormat = " ";
            }
        }

        private void dtpKT_ValueChanged(object sender, EventArgs e)
        {
            dtpKT.CustomFormat = "yyyy-MM-dd";
            btnLoc.Enabled = true;
        }

        private void dtpKT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                dtpKT.CustomFormat = " ";
            }
        }

        private void cmbCoSo_SelectedValueChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            cmbCoSo.SelectedIndex = -1;
            dgvQL.Rows.Clear();
            dgvQL.Refresh();
            ThongKeQuanLy();
            Xoa();
            btnLoc.Enabled = false;
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LocQuanLy();
        }
        private void LocQuanLy()
        {
            try
            {
                dgvQL.Rows.Clear();
                dgvQL.Refresh();
                this.dgvQL.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                List<string> lstMaQL = new List<string>();
                List<string> lstTenQL = new List<string>();
                List<string> lstHoTenLotQL = new List<string>();
                lstMaQL = db.QUAN_LY.Where(x=>x.Hide == false).Select(x => x.MaQL).ToList();
                lstTenQL = db.QUAN_LY.Where(x => x.Hide == false).Select(x => x.Ten).ToList();
                lstHoTenLotQL = db.QUAN_LY.Where(x => x.Hide == false).Select(x => x.HoTenLot).ToList();
                for (int j = 0; j < lstMaQL.Count; j++)
                {
                    string MaQL = lstMaQL[j];
                    string tenQL = lstTenQL[j];
                    string HoTenLotQL = lstHoTenLotQL[j];
                    dgvQL.Rows.Add();
                    dgvQL.Rows[j].Cells[0].Value = j + 1;
                    dgvQL.Rows[j].Cells[1].Value = MaQL;
                    dgvQL.Rows[j].Cells[2].Value = HoTenLotQL;
                    dgvQL.Rows[j].Cells[3].Value = tenQL;
                    if (cmbCoSo.SelectedIndex == -1)
                    { 
                        List<string> coSo = (from s in db.QUAN_LY
                                             where s.MaQL == MaQL && s.Hide == false
                                             select (s.CO_SO1.TenCoSo)).ToList();
                        dgvQL.Rows[j].Cells[4].Value = coSo[0];
                    }
                    else
                    {   
                        string maCoSo = cmbCoSo.SelectedValue.ToString();
                        //MessageBox.Show(maCoSo);
                        List<string> coso = (from s in db.QUAN_LY
                                             where s.MaQL == MaQL && s.CoSo == maCoSo && s.Hide == false
                                             select (s.CO_SO1.TenCoSo)).ToList();
                        if (coso.Count == 0) { dgvQL.Rows[j].Cells[4].Value = " "; }
                        else dgvQL.Rows[j].Cells[4].Value = coso[0];
                    }
                    List<int> lstMaHD = new List<int>();
                    if (dtpBD.Text == " " && dtpKT.Text == " ")
                    {
                        lstMaHD = (from s in db.QUAN_LY
                                   join b in db.HD_QUANLY on s.MaQL equals b.MaQL 
                                   where s.MaQL == MaQL && b.HOAT_DONG.Hide == false
                                   select (b.MaHD)).ToList();
                    }
                    else if (dtpBD.Text != " " && dtpKT.Text == " ")
                    {
                        DateTime BD = Convert.ToDateTime(dtpBD.Text);
                        lstMaHD = (from s in db.QUAN_LY
                                   join b in db.HD_QUANLY on s.MaQL equals b.MaQL
                                   join d in db.HOAT_DONG on b.MaHD equals d.MaHD
                                   where s.MaQL == MaQL && d.NgayBatDau >= BD && b.HOAT_DONG.Hide == false
                                   select (b.MaHD)).ToList();
                    }
                    else if (dtpBD.Text == " " && dtpKT.Text != " ")
                    {
                        DateTime KT = Convert.ToDateTime(dtpKT.Text);
                        lstMaHD = (from s in db.QUAN_LY
                                   join b in db.HD_QUANLY on s.MaQL equals b.MaQL
                                   join d in db.HOAT_DONG on b.MaHD equals d.MaHD
                                   where s.MaQL == MaQL && d.NgayKetThuc <= KT && b.HOAT_DONG.Hide == false
                                   select (b.MaHD)).ToList();

                    }
                    else if (dtpBD.Text != " " && dtpKT.Text != " ")
                    {
                        DateTime BD = Convert.ToDateTime(dtpBD.Text);
                        DateTime KT = Convert.ToDateTime(dtpKT.Text);
                        lstMaHD = (from s in db.QUAN_LY
                                   join b in db.HD_QUANLY on s.MaQL equals b.MaQL
                                   join d in db.HOAT_DONG on b.MaHD equals d.MaHD
                                   where s.MaQL == MaQL && d.NgayBatDau >= BD && d.NgayKetThuc <= KT && b.HOAT_DONG.Hide == false
                                   select (b.MaHD)).ToList();
                    }
                    if (lstMaHD.Count == 0) dgvQL.Rows[j].Cells[5].Value = " ";
                    else
                    {
                        string TenHD = "- ";
                        int MaHD = lstMaHD[0];
                        List<string> NameHD = (from s in db.HOAT_DONG
                                               where s.MaHD == MaHD && s.Hide == false
                                               select (s.TenHoatDong)).ToList();
                        TenHD = TenHD + NameHD[0];
                        for (int i = 1; i < lstMaHD.Count; i++)
                        {
                            MaHD = lstMaHD[i];
                            List<string> TenHoat = (from s in db.HOAT_DONG
                                                    where s.MaHD == MaHD && s.Hide == false
                                                    select (s.TenHoatDong)).ToList();
                            TenHD = TenHD + "\n- " + TenHoat[0];
                        }
                        dgvQL.Rows[j].Cells[5].Value = TenHD;
                    }
                }
               Xoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void dgvQL_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
