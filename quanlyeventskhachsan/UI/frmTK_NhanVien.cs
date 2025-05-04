using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using quanlyeventskhachsan.MODEL;

namespace quanlyeventskhachsan.UI
{
    public partial class frmTK_NhanVien : Form
    {
        Context db = new Context();
        public frmTK_NhanVien()
        {
            InitializeComponent();
        }
        private void frmTK_NhanVien_Load(object sender, EventArgs e)
        {
            LoadLoai();
            txtSearch.Text = "";
            cmbCoSo.SelectedIndex = -1;
            DisplayCMBCoSo(cmbCoSo);
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            ThongkeNhanVien();
            Xoa();
            btnLoc.Enabled = false;
        }
        public void DisplayCMBCoSo(ComboBox a)
        {
            var kh = db.CO_SOs.Select(s => s).Where(s=>s.Hide == false);
            a.DataSource = kh.ToList();
            a.ValueMember = "MaCoSo";
            a.DisplayMember = "TenCoSo";
            a.SelectedIndex = -1;

        }
        public void LoadLoai()
        {
            cmbLoai.Items.Add("Sinh nhật");
            cmbLoai.Items.Add("Sự kiện");
            cmbLoai.Items.Add("Đám cưới");
        }
        private void ThongkeNhanVien()
        {
            dgvNV.Rows.Clear();
            dgvNV.Refresh();
            this.dgvNV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            List<NHAN_VIEN> lstNV = new List<NHAN_VIEN>();
            using (Context db = new Context())
            {
                lstNV = db.NHAN_VIEN.Where(x => x.Hide == false).Select(x => x).Take(200).ToList();
                for (int j = 0; j < lstNV.Count; j++)
                {
                    NHAN_VIEN NV = lstNV[j];
                    if (NV.CO_SO1 == null)
                    {
                        dgvNV.Rows.Add();
                        continue;
                    }
                    dgvNV.Rows.Add();
                    dgvNV.Rows[j].Cells[0].Value = j + 1;
                    dgvNV.Rows[j].Cells[1].Value = NV.MSNV;
                    dgvNV.Rows[j].Cells[2].Value = NV.HoTen;
                    dgvNV.Rows[j].Cells[3].Value = NV.CO_SO1.TenCoSo;
                    List<int> lstMaHD = new List<int>();
                    List<HD_NHAN_VIEN> lstHD = new List<HD_NHAN_VIEN>();
                    if (NV.HD_NHAN_VIEN.Count == 0)
                    {
                        dgvNV.Rows[j].Cells[4].Value = " ";
                        dgvNV.Rows[j].Cells[5].Value = " ";
                    }
                    else
                    {
                        lstHD = NV.HD_NHAN_VIEN.Where(x=>x.HOAT_DONG.Hide == false).ToList();
                        string vaitro = "- ";
                        string TenHD = "- ";

                        vaitro = vaitro + String.Join("\n- ", lstHD.Select(x => x.VaiTro));
                        TenHD = TenHD + String.Join("\n- ", lstHD.Select(x => x.HOAT_DONG.TenHoatDong));

                        if (vaitro.Equals("- ") && TenHD.Equals("- "))
                            vaitro = TenHD = "";

                        dgvNV.Rows[j].Cells[4].Value = vaitro;
                        dgvNV.Rows[j].Cells[5].Value = TenHD;
                    }
                }
            }
            Xoa();
        }
        private void LocThongkeNhanVien()
        {
            try
            {
                dgvNV.Rows.Clear();
                dgvNV.Refresh();
                this.dgvNV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                List<string> lstMaNV = new List<string>();
                List<string> lstHoTenNV = new List<string>();
                if (string.IsNullOrEmpty(txtSearch.Text) != true)
                {
                    string msnv = txtSearch.Text;
                    lstMaNV = db.NHAN_VIEN.Where(x => x.Hide == false && x.MSNV.Contains(msnv)).Select(x => x.MSNV).Take(200).ToList();
                    lstHoTenNV = db.NHAN_VIEN.Where(x => x.Hide == false && x.MSNV.Contains(msnv)).Select(x => x.HoTen).Take(200).ToList();
                }
                else
                {
                    lstMaNV = db.NHAN_VIEN.Where(x => x.Hide == false).Select(x => x.MSNV).Take(200).ToList();
                    lstHoTenNV = db.NHAN_VIEN.Where(x => x.Hide == false).Select(x => x.HoTen).Take(200).ToList();
                }
                for (int j = 0; j < lstMaNV.Count; j++)
                {
                    string MaNV = lstMaNV[j];
                    string HoTenNV = lstHoTenNV[j];
                    dgvNV.Rows.Add();
                    dgvNV.Rows[j].Cells[0].Value = j + 1;
                    dgvNV.Rows[j].Cells[1].Value = MaNV;
                    dgvNV.Rows[j].Cells[2].Value = HoTenNV;
                    if(cmbCoSo.SelectedIndex != -1)
                    {
                       string cs = cmbCoSo.SelectedValue.ToString();
                       // MessageBox.Show(cs);
                       List<string> CoSo = (from s in db.NHAN_VIEN
                                             join b in db.CO_SOs on s.CoSo equals b.MaCoSo
                                             where s.MSNV == MaNV && b.Hide == false && s.CoSo == cs
                                             select (b.TenCoSo)).ToList();
                       if (CoSo.Count == 0) { dgvNV.Rows[j].Cells[3].Value = " "; }
                       else dgvNV.Rows[j].Cells[3].Value = CoSo[0];
                    }
                    else
                    {
                       List<string> coso = (from s in db.NHAN_VIEN
                                             join b in db.CO_SOs on s.CoSo equals b.MaCoSo
                                             where s.MSNV == MaNV && b.Hide == false
                                             select (b.TenCoSo)).ToList();
                       dgvNV.Rows[j].Cells[3].Value = coso[0];
                    }
                    List<int> lstMaHD = new List<int>();
                    if (cmbLoai.SelectedIndex == -1 && dtpBD.Text == " " && dtpKT.Text == " ")
                    {
                            lstMaHD = (from s in db.NHAN_VIEN
                                       join b in db.HD_NHAN_VIEN on s.MSNV equals b.MSNV
                                       join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                       where s.MSNV == MaNV && c.Hide == false 
                                       select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex != -1 && dtpBD.Text == " " && dtpKT.Text == " ")
                    {
                            string loai = cmbLoai.SelectedItem.ToString();
                            lstMaHD = (from s in db.NHAN_VIEN
                                       join b in db.HD_NHAN_VIEN on s.MSNV equals b.MSNV
                                       join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                       where s.MSNV == MaNV && c.Hide == false && c.Loai == loai
                                       select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex != -1 && dtpBD.Text != " " && dtpKT.Text == " ")
                    {
                            string loai = cmbLoai.SelectedItem.ToString();
                            DateTime BD = Convert.ToDateTime(dtpBD.Text);
                            lstMaHD = (from s in db.NHAN_VIEN
                                       join b in db.HD_NHAN_VIEN on s.MSNV equals b.MSNV
                                       join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                       where s.MSNV == MaNV && c.Hide == false && c.NgayBatDau >= BD && c.Loai == loai
                                       select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex != -1 && dtpBD.Text == " " && dtpKT.Text != " ")
                    {
                            string loai = cmbLoai.SelectedItem.ToString();
                            DateTime KT = Convert.ToDateTime(dtpKT.Text);
                            lstMaHD = (from s in db.NHAN_VIEN
                                       join b in db.HD_NHAN_VIEN on s.MSNV equals b.MSNV
                                       join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                       where s.MSNV == MaNV && c.Hide == false && c.NgayKetThuc <= KT && c.Loai == loai
                                       select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex == -1 && dtpBD.Text != " " && dtpKT.Text == " ")
                    {
                            DateTime BD = Convert.ToDateTime(dtpBD.Text);
                            lstMaHD = (from s in db.NHAN_VIEN
                                       join b in db.HD_NHAN_VIEN on s.MSNV equals b.MSNV
                                       join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                       where s.MSNV == MaNV && c.Hide == false && c.NgayBatDau >= BD
                                       select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex == -1 && dtpBD.Text == " " && dtpKT.Text != " ")
                    {
                            DateTime KT = Convert.ToDateTime(dtpKT.Text);
                            lstMaHD = (from s in db.NHAN_VIEN
                                       join b in db.HD_NHAN_VIEN on s.MSNV equals b.MSNV
                                       join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                       where s.MSNV == MaNV && c.Hide == false && c.NgayKetThuc <= KT
                                       select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex == -1 && dtpBD.Text != " " && dtpKT.Text != " ")
                    {

                            DateTime BD = Convert.ToDateTime(dtpBD.Text);
                            DateTime KT = Convert.ToDateTime(dtpKT.Text);
                            // MessageBox.Show(BD.ToString());
                            lstMaHD = (from s in db.NHAN_VIEN
                                       join b in db.HD_NHAN_VIEN on s.MSNV equals b.MSNV
                                       join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                       where s.MSNV == MaNV && c.Hide == false && c.NgayBatDau >= BD && c.NgayKetThuc <= KT
                                       select (c.MaHD)).ToList();
                    }
                    else if (cmbLoai.SelectedIndex != -1 && dtpBD.Text != " " && dtpKT.Text != " ")
                    {
                            string loai = cmbLoai.SelectedItem.ToString();
                            DateTime BD = Convert.ToDateTime(dtpBD.Text);
                            DateTime KT = Convert.ToDateTime(dtpKT.Text);
                            // MessageBox.Show(BD.ToString());
                            lstMaHD = (from s in db.NHAN_VIEN
                                       join b in db.HD_NHAN_VIEN on s.MSNV equals b.MSNV
                                       join c in db.HOAT_DONG on b.MaHD equals c.MaHD
                                       where s.MSNV == MaNV && c.Hide == false && c.NgayBatDau >= BD && c.NgayKetThuc <= KT && c.Loai == loai
                                       select (c.MaHD)).ToList();
                    }
                    if (lstMaHD.Count == 0)
                    {
                        dgvNV.Rows[j].Cells[4].Value = " ";
                        dgvNV.Rows[j].Cells[5].Value = " ";
                    }
                    else
                    {
                        string vaitro = "- ";
                        string TenHD = "- ";
                        int MaHD = lstMaHD[0];
                        List<string> lstvaitro = (from s in db.HOAT_DONG
                                                  join b in db.HD_NHAN_VIEN on s.MaHD equals b.MaHD
                                                  where s.MaHD == MaHD && b.MSNV == MaNV && s.Hide == false
                                                  select (b.VaiTro)).ToList();
                        vaitro = vaitro + lstvaitro[0];
                        List<string> NameHD = (from s in db.HOAT_DONG
                                               where s.MaHD == MaHD && s.Hide == false
                                               select (s.TenHoatDong)).ToList();
                        TenHD = TenHD + NameHD[0];
                        for (int i = 1; i < lstMaHD.Count; i++)
                        {
                            MaHD = lstMaHD[i];
                            List<string> TenVai = (from s in db.HOAT_DONG
                                                   join b in db.HD_NHAN_VIEN on s.MaHD equals b.MaHD
                                                   where s.MaHD == MaHD && b.MSNV == MaNV && s.Hide == false
                                                   select (b.VaiTro)).ToList();
                            vaitro = vaitro + "\n- " + TenVai[0];
                            List<string> TenHoat = (from s in db.HOAT_DONG
                                                    where s.MaHD == MaHD && s.Hide == false
                                                    select (s.TenHoatDong)).ToList();
                            TenHD = TenHD + "\n- " + TenHoat[0];
                        }
                        dgvNV.Rows[j].Cells[4].Value = vaitro;
                        dgvNV.Rows[j].Cells[5].Value = TenHD;
                    }
                }
                Xoa();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Xoa()
        {
            int n = dgvNV.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                if (dgvNV.Rows[i].Cells[5].Value == " " || dgvNV.Rows[i].Cells[3].Value == " ")
                {
                    //Object stt = dgvNV.Rows[i].Cells[0].Value;
                    dgvNV.Rows.RemoveAt(dgvNV.Rows[i].Index);
                    i--;
                    n--;
                   // dgvNV.Rows[i+1].Cells[0].Value = stt;
                }
            }
            for (int i=0;i<n-1;i++)
            {
                dgvNV.Rows[i].Cells[0].Value = i + 1;
            }    
        }
        private void btnLoc_Click(object sender, EventArgs e)
        {
            LocThongkeNhanVien();
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

        private void cmbCoSo_TextChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }

        private void cmbLoai_TextChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            cmbLoai.SelectedIndex = -1;
            txtSearch.Text = "";
            dtpBD.CustomFormat = " ";
            dtpKT.CustomFormat = " ";
            cmbCoSo.SelectedIndex = -1;
            dgvNV.Rows.Clear();
            dgvNV.Refresh();
            ThongkeNhanVien();
            Xoa();
            btnLoc.Enabled = false;
        }

        private void cmbCoSo_SelectedValueChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }

        private void cmbLoai_SelectedValueChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*" })
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToExcel(dgvNV, sfd.FileName);
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
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }
    }
}
