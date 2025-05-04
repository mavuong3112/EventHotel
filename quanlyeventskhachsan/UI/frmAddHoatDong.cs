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
using TextBox = System.Windows.Forms.TextBox;
using OfficeOpenXml;
using ComponentFactory.Krypton.Toolkit;
using System.IO;
using System.Xml.Linq;
using TheArtOfDevHtmlRenderer.Core;
using quanlyeventskhachsan.MODEL;

namespace quanlyeventskhachsan.UI
{
    public partial class frmAddHoatDong : Form
    {
        private int id;
        public bool isCreate = true;
        public int DT_ID = -1;
        public int TT_ID = -1;
        public string iniName = ""; //luu ten hd truoc khi sua
        public frmAddHoatDong()
        {
            InitializeComponent();
            this.Text = "Tạo Mới Hoạt Động";
        }
        public void LoadFormUpdate(int idHD)
        {
            id = idHD;
            try
            {
                LoadCoSoCB();
                using (Context db = new Context())
                {
                    this.Text = "Sửa Hoạt Động";
                    HOAT_DONG hD = db.HOAT_DONG.Find(idHD);
                    lblHeader.Text = "Sửa Hoạt Động";
                    //txtMaHD.Text = hD.MaHD.Trim(); txtMaHD.ReadOnly = true;
                    iniName = hD.TenHoatDong;
                    txtTenHD.Text = hD.TenHoatDong;
                    cbLoai.Text = hD.Loai == null ? "" : hD.Loai;
                    dtpNgayBD.Value = hD.NgayBatDau == null ? DateTime.Now : (DateTime)hD.NgayBatDau;
                    dtpNgayKT.Value = hD.NgayKetThuc == null ? DateTime.Now : (DateTime)hD.NgayKetThuc;
                    LoadHD_NhanVien(hD);
                    LoadHD_QL(hD);
                    LoadHD_DT(hD);
                    LoadHD_TT(hD);
                    LoadHD_TC(hD);
                    btnAddHD.Text = "Sửa Hoạt Động";
                    isCreate = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading form:\n"+ex.Message,"Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        public void LoadFormView(string idHD)
        {
            try
            {
                this.Text = "Thông Tin Hoạt Động";
                lblHeader.Text = "Thông Tin Hoạt Động";
                btnAddHD.Visible = false;
                isCreate = false;
                using (Context db = new Context())
                {
                    HOAT_DONG hD = db.HOAT_DONG.Find(idHD);
                    //txtMaHD.Text = hD.MaHD.Trim();
                    txtTenHD.Text = hD.TenHoatDong;
                    cbLoai.Text = hD.Loai == null ? "" : hD.Loai;
                    dtpNgayBD.Value = hD.NgayBatDau == null ? DateTime.Now : (DateTime)hD.NgayBatDau;
                    dtpNgayKT.Value = hD.NgayKetThuc == null ? DateTime.Now : (DateTime)hD.NgayKetThuc;
                    LoadHD_NhanVien(hD);
                    LoadHD_QL(hD);
                    LoadHD_DT(hD);
                    LoadHD_TT(hD);
                    LoadHD_TC(hD);
                    lblNV_TotalNumber.Text = dgvNhanVien.Rows.Count.ToString();
                    lblQL_TotalNumber.Text = dgv_QL.Rows.Count.ToString();

                    foreach (Control g in panel1.Controls)
                    {
                        if (g is GroupBox)
                        {
                            foreach (Control c in g.Controls)
                            {
                                if(c is TextBox)
                                {
                                    TextBox a = c as TextBox;
                                    a.ReadOnly = true;
                                }
                                if (c is Button | c is KryptonButton |c is ComboBox | c is DateTimePicker)
                                {
                                    c.Enabled = false;  
                                }
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
                dgvNhanVien.Rows.Add(NV.MSNV, NV.NHAN_VIEN.HoTen, NV.NHAN_VIEN.CO_SO1.TenCoSo, NV.VaiTro, NV.GhiChu, NV.NHAN_VIEN.CoSo);
            }
        }
        
        private void LoadHD_QL(HOAT_DONG hD)
        {    //MaQL,HoTenLot,Ten,GVKhoa,GV_DonVi,GVKHoa_DB
            List<HD_QUANLY> List = hD.HD_QUANLY.ToList();
            foreach (HD_QUANLY QL in List)
            {
                if (QL.QUAN_LY == null || QL.QUAN_LY.CO_SO1 == null || QL.QUAN_LY.CO_SO1.Hide == true) { continue; }

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
                dgvTaiTro.Rows.Add(TT.TAI_TRO.TenTaiTro, TT.TAI_TRO.DaiDien, TT.TAI_TRO.SDT, TT.TAI_TRO.Email, TT.TAI_TRO.ID_TaiTro, TT.NoiDung);
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

        private void frmAddHoatDong_Load(object sender, EventArgs e)
        {
            if (!isCreate) return;
            LoadCoSoCB();
           // btnShowNV.PerformClick();
        }
        private void LoadLoaiCB()
        {
            cbLoai.DataSource = new Dictionary<int, string>()
            {
                {0, "Đám Cưới"},
                {1, "Sinh Nhật"},
                {2, "Khác"},
            }.ToList();
            cbLoai.DisplayMember = "Value";
            cbLoai.ValueMember = "Key";
            cbLoai.SelectedIndex = -1;
        }
        private void LoadVaiTroCB()
        {
            cbRole.DataSource = new Dictionary<int, string>()
            {
                {0, "Tham gia"},
                {1, "Tổ chức"},
            }.ToList();
            cbRole.DisplayMember = "Value";
            cbRole.ValueMember = "Key";
            cbRole.SelectedIndex = -1;
        }

        private void LoadCoSoCB()
        {
            try
            {
                using (Context db = new Context())
                {
                    var cs = from c in db.CO_SOs
                               where (c.Hide == false)
                               select new
                               {
                                   MaCS = c.MaCoSo,
                                   Ten = c.TenCoSo,
                               };
                    cbCoSo.DataSource = cs.ToList();
                    cbCoSo.DisplayMember = "Ten";
                    cbCoSo.ValueMember = "MaCS";
                    cbCoSo.SelectedIndex = -1;

                    cbQL_CoSo.DataSource = cs.ToList();
                    cbQL_CoSo.DisplayMember = "Ten";
                    cbQL_CoSo.ValueMember = "MaCS";
                    cbQL_CoSo.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Cơ Sở:\n"+ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnAddNV_Click(object sender, EventArgs e)
        {
            if (ValidateNV())
            { //
                dgvNhanVien.Rows.Add(txtMSNV.Text, txtNVHoTen.Text, cbCoSo.Text, cbRole.Text, txtNVNotes.Text, cbCoSo.SelectedValue, cbRole.SelectedValue);
                clearNVFields();
                lblNV_TotalNumber.Text = dgvNhanVien.RowCount.ToString();
            }
            else return;
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvNhanVien.CurrentRow;
            if (row != null)
            {
                txtMSNV.Text = row.Cells["MSNV"].Value.ToString();
                txtMSNV.ReadOnly = true;
                txtNVHoTen.Text = row.Cells["HoTenNV"].Value.ToString();
                cbCoSo.SelectedValue = row.Cells["DB_CoSo"].Value != null? row.Cells["DB_CoSo"].Value : -1;
                cbRole.Text = row.Cells["Role"].Value.ToString() != null ? row.Cells["Role"].Value.ToString() : "";
                txtNVNotes.Text = row.Cells["Notes_NV"].Value.ToString();
                btnAddNV.Enabled = false;
            }
            else
                return;
            
        }

        private void kryptonBtnEdit_Click(object sender, EventArgs e)
        {
            if(txtMSNV.ReadOnly == false|| dgvNhanVien.CurrentRow == null) return;
            dgvNhanVien.CurrentRow.Cells["HoTenNV"].Value = txtNVHoTen.Text;
            dgvNhanVien.CurrentRow.Cells["CoSo"].Value = cbCoSo.Text;
            dgvNhanVien.CurrentRow.Cells["Role"].Value = cbRole.Text;
            dgvNhanVien.CurrentRow.Cells["Notes_NV"].Value = txtNVNotes.Text;
            dgvNhanVien.CurrentRow.Cells["DB_CoSo"].Value = cbCoSo.SelectedValue;
            clearNVFields();
        }
        public void clearNVFields()
        {
            txtMSNV.Clear();
            txtNVHoTen.Clear();
            cbCoSo.SelectedIndex = -1;
            cbCoSo.Text = string.Empty;
            cbRole.SelectedIndex = -1;
            cbRole.Text = string.Empty;
            txtNVNotes.Clear();
            txtMSNV.ReadOnly = false;
            btnAddNV.Enabled = true;
        }
        public bool ValidateNV()
        {
            if(txtMSNV.Text.Length == 0) 
            {
                MessageBox.Show("MSNV Đang Trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else 
            if (FindDuplicateMSNV(txtMSNV.Text.Trim()))
            {
                MessageBox.Show("MSNV Đang Bị Trùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }else
                return true;
        }
        public bool FindDuplicateMSNV(string MSNV)
        {
            foreach (DataGridViewRow row in dgvNhanVien.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["MSNV"].Value.ToString().Trim() == MSNV)
                    {
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }
        //Cập nhật dgv_GV và trả về kết quả
        //Nếu không cập nhật được/không tìm thấy hàng nào với mã QL đã cho, trả về false
        //Nếu cập nhật thành công, return true
        //MaQL,HoTenLot,Ten,GVKhoa,QL_Role,GVKHoa_DB
        public bool Updatedgv_QL(DataRow QL)
        {
            foreach (DataGridViewRow row in dgv_QL.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["MaQL"].Value.ToString().Trim() == QL[0].ToString())
                    {
                        row.Cells["HoTenLot"].Value = QL[1].ToString();
                        row.Cells["Ten"].Value = QL[2].ToString();
                        row.Cells["QLCoSo"].Value = QL[3].ToString();
                        row.Cells["QL_Role"].Value = QL[4].ToString();
                        row.Cells["QLCoSo_DB"].Value = QL[5].ToString();
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }
        //MSNV,HoTenNV,Khoa,Role,Notes_NV,DB_Khoa
        public bool UpdateDgv_NV(DataRow nv)
        {
            foreach (DataGridViewRow row in dgvNhanVien.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["MSNV"].Value.ToString().Trim() == nv[0].ToString().Trim())
                    {
                        row.Cells["HoTenNV"].Value = nv[1].ToString().Trim();
                        row.Cells["CoSo"].Value = nv[2].ToString().Trim();
                        row.Cells["Role"].Value = nv[3].ToString().Trim();
                        row.Cells["Notes_NV"].Value = nv[4].ToString().Trim();
                        row.Cells["DB_CoSo"].Value = nv[5].ToString().Trim();
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }

        public bool FindDuplicateDT(DataRow DT)
        {
            foreach (DataGridViewRow row in dgvDoiTac.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells[0].Value.ToString().Trim() == DT[0].ToString().Trim() && row.Cells[1].Value.ToString().Trim() == DT[1].ToString().Trim() && row.Cells[2].Value.ToString().Trim() == DT[2].ToString().Trim() && row.Cells[3].Value.ToString().Trim() == DT[3].ToString().Trim())
                    {
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }
        public bool FindDuplicateTT(DataRow DT)
        {
            foreach (DataGridViewRow row in dgvTaiTro.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells[0].Value.ToString().Trim() == DT[0].ToString().Trim() && row.Cells[1].Value.ToString().Trim() == DT[1].ToString().Trim() && row.Cells[2].Value.ToString().Trim() == DT[2].ToString().Trim() && row.Cells[3].Value.ToString().Trim() == DT[3].ToString().Trim())
                    {
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }

        private void btnNVClear_Click(object sender, EventArgs e)
        {
            clearNVFields();
        }

        private void btnNVDel_Click(object sender, EventArgs e)
        {
            clearNVFields();
            if (dgvNhanVien.Rows.Count == 0 || dgvNhanVien.CurrentRow.Index < 0) return;  
            dgvNhanVien.Rows.RemoveAt(dgvNhanVien.CurrentRow.Index);
            lblNV_TotalNumber.Text = dgvNhanVien.RowCount.ToString();
        }

        private bool frmValidate()
        {
            if (isEmpty())
                return false;
            else
                return true;
        }
        private bool isEmpty()
        {
            if (txtTenHD.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Tên HD đang trống","Cảnh báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                } 
            else return false;
        }
        private void btnAddHD_Click(object sender, EventArgs e)
        {

            //Validate Here()
            if (!frmValidate())
                return;
            else if (!isCreate)
            {
                DeleteAllMembers();
                SaveHDToDB();
            }
            else
                SaveHDToDB();
           // else return;
        }
        private void SaveHDToDB()
        {

            try
            {
                using (Context db = new Context())
                {   //tim hoat dong theo ten va chua bi xoa
                    HOAT_DONG hoatDong = isCreate? new HOAT_DONG() : db.HOAT_DONG.Where<HOAT_DONG>(hd=>hd.Hide.Value != true && hd.TenHoatDong.Contains(iniName)).FirstOrDefault();
                    hoatDong.TenHoatDong = txtTenHD.Text.Trim();
                    hoatDong.Loai = cbLoai.Text.Trim();
                    hoatDong.NgayBatDau = dtpNgayBD.Value;
                    hoatDong.NgayKetThuc = dtpNgayKT.Value;
                    hoatDong.CreatedDate = DateTime.Now;
                    hoatDong.Hide = false;
                    if (isCreate) //lưu hoạt động vào DB để tạo ID trước
                    {
                        db.HOAT_DONG.Add(hoatDong);
                        db.SaveChanges();
                    }    
                    AddOrUpdateHD_NhanVien(hoatDong,db);
                    AddOrUpdateHD_QL(hoatDong,db);
                    AddOrUpdateHD_DoiTac(hoatDong, db);
                    AddOrUpdateHD_TaiTro(hoatDong, db);
                    AddHD_TaiChinh(hoatDong, db);

                    if (db.HOAT_DONG.Find(hoatDong.MaHD) != null)
                    {
                        db.Entry(hoatDong).State = EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Thành công", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                       // hoatDong.MaHD = txtMaHD.Text.Trim();
                        db.HOAT_DONG.Add(hoatDong);
                        db.SaveChanges();
                        MessageBox.Show("Thành công","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    Close();
                }    
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AddOrUpdateHD_NhanVien(HOAT_DONG hd, Context db)
        {
                foreach (DataGridViewRow dr in dgvNhanVien.Rows)
                {
                    HD_NHAN_VIEN NVList = db.HD_NHAN_VIEN.Find(hd.MaHD, dr.Cells["MSNV"].Value.ToString());
                    if (NVList == null)
                    {
                        NVList = new HD_NHAN_VIEN();
                        NVList.MaHD = hd.MaHD;
                        NVList.MSNV = dr.Cells["MSNV"].Value.ToString().Trim();
                        NVList.VaiTro = dr.Cells["Role"].Value.ToString().Trim();
                        NVList.GhiChu = dr.Cells["Notes_NV"].Value.ToString().Trim();
                        AddOrUpdateNV(NVList, db, dr);
                        hd.HD_NHAN_VIEN.Add(NVList);
                    }
                    else
                    {
                        NVList.VaiTro = dr.Cells["Role"].Value.ToString().Trim();
                        NVList.GhiChu = dr.Cells["Notes_NV"].Value.ToString().Trim();
                        AddOrUpdateNV(NVList, db, dr);
                        db.Entry(NVList).State = EntityState.Modified;
                        db.SaveChanges();
                    }
            }  
        }
        private void AddOrUpdateNV(HD_NHAN_VIEN lst, Context db, DataGridViewRow dr)
        {
            if (db.NHAN_VIEN.Find(dr.Cells["MSNV"].Value.ToString()) != null)
            {
                NHAN_VIEN nv = db.NHAN_VIEN.Find(lst.MSNV);
                nv.HoTen = dr.Cells["HoTenNV"].Value.ToString().Trim();
                nv.CoSo = dr.Cells["DB_CoSo"].Value.ToString().Trim();
                nv.Hide = false;
                db.Entry(nv).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                lst.NHAN_VIEN = new NHAN_VIEN();
                lst.NHAN_VIEN.MSNV = dr.Cells["MSNV"].Value.ToString().Trim();
                lst.NHAN_VIEN.HoTen = dr.Cells["HoTenNV"].Value.ToString().Trim();
                lst.NHAN_VIEN.CoSo = dr.Cells["DB_CoSo"].Value.ToString().Trim();
                lst.NHAN_VIEN.Hide = false;

            }
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////
        /// </summary>

        private void AddOrUpdateHD_DoiTac(HOAT_DONG hd, Context db)
        {
            foreach (DataGridViewRow dr in dgvDoiTac.Rows)
            {
                DOI_TAC dtTemp = FindDTByName(dr.Cells["DT_Ten"].Value.ToString().Trim());
                if (dtTemp != null) dr.Cells["ID_DB"].Value = dtTemp.ID_DoiTac;
                else dr.Cells["ID_DB"].Value = -1;
                //cho trường hợp nhập vào bằng import excel, ko cần ID, nếu ĐT đã có sẵn trong CSDL, tự điền lại ID của đối tác đó vào
                //Nếu đối tác đó không có sẵn, add vào như bth
                HD_DOITAC hD_DT = db.HD_DOITAC.Find(dr.Cells["ID_DB"].Value, hd.MaHD);
                if (hD_DT == null)
                {
                    hD_DT = new HD_DOITAC();
                    hD_DT.MaHD = hd.MaHD;
                    AddOrUpdateDT(hD_DT, db, dr);
                    hD_DT.ID_DoiTac = hD_DT.DOI_TAC.ID_DoiTac;
                    hD_DT.NoiDung = dr.Cells["DT_NoiDung"].Value.ToString().Trim();
                    hd.HD_DOITAC.Add(hD_DT);
                    db.SaveChanges();
                }
                else
                {
                    hD_DT.NoiDung = dr.Cells["DT_NoiDung"].Value.ToString().Trim();
                    AddOrUpdateDT(hD_DT, db, dr);
                    db.Entry(hD_DT).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }        //DT_Ten,DT_DaiDien,DT_SDT,DT_Email,DT_NoiDung,ID_DB
        private void AddOrUpdateDT(HD_DOITAC lst, Context db, DataGridViewRow dr)
        {
            if (db.DOI_TAC.Find(dr.Cells["ID_DB"].Value) != null)
            {
                DOI_TAC dt = db.DOI_TAC.Find(dr.Cells["ID_DB"].Value);
                dt.TenDoiTac = dr.Cells["DT_Ten"].Value.ToString().Trim();
                dt.DaiDien = dr.Cells["DT_DaiDien"].Value.ToString().Trim();
                dt.SDT = dr.Cells["DT_SDT"].Value.ToString().Trim();
                dt.Email = dr.Cells["DT_Email"].Value.ToString().Trim();
                dt.Hide = false;
                db.Entry(dt).State = EntityState.Modified;
                lst.DOI_TAC = dt;
                db.SaveChanges();
            }
            else
            {
                lst.DOI_TAC = new DOI_TAC();
                lst.DOI_TAC.TenDoiTac = dr.Cells["DT_Ten"].Value.ToString().Trim();
                lst.DOI_TAC.DaiDien = dr.Cells["DT_DaiDien"].Value.ToString().Trim();
                lst.DOI_TAC.SDT = dr.Cells["DT_SDT"].Value.ToString().Trim();
                lst.DOI_TAC.Email = dr.Cells["DT_Email"].Value.ToString().Trim();
                lst.DOI_TAC.Hide = false;
                db.DOI_TAC.Add(lst.DOI_TAC);
                db.SaveChanges();
            }
        }

        //TT_Name, TT_Rep, TT_SDT, TT_Email, TT_Notes, TT_IDDB
        private void AddOrUpdateHD_TaiTro(HOAT_DONG hd, Context db)
        {
            foreach (DataGridViewRow dr in dgvTaiTro.Rows)
            {
                TAI_TRO dtTemp = FindTTByName(dr.Cells["TT_Name"].Value.ToString().Trim());
                if (dtTemp != null) dr.Cells["TT_IDDB"].Value = dtTemp.ID_TaiTro;
                HD_TAITRO hD_TT = db.HD_TAITRO.Find(dr.Cells["TT_IDDB"].Value, hd.MaHD);
                if (hD_TT == null)
                {
                    hD_TT = new HD_TAITRO();
                    hD_TT.MaHD = hd.MaHD;
                    AddOrUpdateTT(hD_TT, db, dr);
                    hD_TT.ID_TaiTro = hD_TT.TAI_TRO.ID_TaiTro;
                    hD_TT.NoiDung = dr.Cells["TT_Notes"].Value.ToString().Trim();
                    hd.HD_TAITRO.Add(hD_TT);
                    db.SaveChanges();
                }
                else
                {
                    hD_TT.NoiDung = dr.Cells["TT_Notes"].Value.ToString().Trim();
                    AddOrUpdateTT(hD_TT, db, dr);
                    db.Entry(hD_TT).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }     
        private void AddOrUpdateTT(HD_TAITRO lst, Context db, DataGridViewRow dr)
        {
            if (db.TAI_TRO.Find(dr.Cells["TT_IDDB"].Value) != null)
            {
                TAI_TRO tt = db.TAI_TRO.Find(dr.Cells["TT_IDDB"].Value);
                tt.TenTaiTro = dr.Cells["TT_Name"].Value.ToString().Trim();
                tt.DaiDien = dr.Cells["TT_Rep"].Value.ToString().Trim();
                tt.SDT = dr.Cells["TT_SDT"].Value.ToString().Trim();
                tt.Email = dr.Cells["TT_Email"].Value.ToString().Trim();
                tt.Hide = false;
                lst.TAI_TRO = tt;
                db.Entry(tt).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                lst.TAI_TRO = new TAI_TRO();
                lst.TAI_TRO.TenTaiTro = dr.Cells["TT_Name"].Value.ToString().Trim();
                lst.TAI_TRO.DaiDien = dr.Cells["TT_Rep"].Value.ToString().Trim();
                lst.TAI_TRO.SDT = dr.Cells["TT_SDT"].Value.ToString().Trim();
                lst.TAI_TRO.Email = dr.Cells["TT_Email"].Value.ToString().Trim();
                lst.TAI_TRO.Hide = false;
                db.TAI_TRO.Add(lst.TAI_TRO);
                db.SaveChanges();
            }
        }

        private void AddHD_TaiChinh (HOAT_DONG hd, Context db)
        {
            TAI_CHINH tc = new TAI_CHINH();
            tc.MaHD = hd.MaHD;
            tc.UEF = numUEF.Value;
            tc.TaiTro = numTaiTro.Value;
            tc.TieuDe = txtTC_TieuDe.Text;
            tc.Khac = txtTC_Khac.Text;
            tc.CreatedDate = DateTime.Now;
            tc.Hide = false;
            db.TAI_CHINH.Add(tc);
            db.SaveChanges();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNVFind_Click(object sender, EventArgs e)
        {
            lblNVFind.Text = "Loading...";
            //If no value is enter for search, disable button
            if (string.IsNullOrEmpty(txtNVHoTen.Text.Trim()) && String.IsNullOrEmpty(txtMSNV.Text.Trim()) && String.IsNullOrEmpty(cbCoSo.Text.ToString().Trim()))
            {
                btnNVFind.Enabled = false;
                lblNVFind.Text = "";
                return;
            }    
            //Filter through each condition
            using (Context db = new Context())
            {
                //initialize data
                lblNVFind.Text = "Init data...";
                var NVdata = (from nv in db.NHAN_VIEN
                                         where nv.Hide == false && nv.CO_SO1.Hide == false
                                         select new
                                         {
                                             MSNV = nv.MSNV,
                                             HoTen = nv.HoTen,
                                             CoSo = nv.CoSo,
                                             TenCoSo = nv.CO_SO1.TenCoSo,
                                         });
                if (NVdata == null) 
                {
                    lblNVFind.Text = "";
                    return;
                }
                lblNVFind.Text = "Đang Lọc...";
                //filter MSNV
                if (!String.IsNullOrEmpty(txtMSNV.Text.Trim()))
                {
                    string msnv = txtMSNV.Text.Trim();
                    NVdata = (from nv in NVdata
                              where nv.MSNV.Contains(msnv)
                              select nv
                              );
                }
                //filter name
                if (!string.IsNullOrEmpty(txtNVHoTen.Text.Trim()))
                {
                    string name = txtNVHoTen.Text.Trim();
                    NVdata = (from nv in NVdata
                              where nv.HoTen.Contains(name)
                              select nv
                              );
                }
                //filter cs
                if (cbCoSo.SelectedIndex != -1)
                {
                    string maCS = cbCoSo.SelectedValue.ToString();
                    NVdata = (from nv in NVdata
                              where nv.CoSo == maCS
                              select nv
                              );
                }
                if (NVdata == null)
                {
                    lblNVFind.Text = "";
                    return;
                }
                var NV = NVdata.FirstOrDefault();
                if (NV == null)
                {
                    lblNVFind.Text = "";
                    return;
                }
                txtNVHoTen.Text = NV.HoTen.ToString();
                txtMSNV.Text = NV.MSNV.ToString();
                txtMSNV.ReadOnly = true;
                txtNVHoTen.Focus();
                cbCoSo.SelectedValue = NV.CoSo;
                btnNVFind.Enabled = false;
                lblNVFind.Text = "";
                /*NHAN_VIEN nv = db.NHAN_VIEN.Find(txtMSNV.Text);
                if (nv != null)
                {
                    txtNVHoTen.Text = nv.HoTen.ToString();
                    cbCoSo.SelectedValue = nv.Khoa;
                }
                else 
                    return;*/

            }
            return;
        }

        private void dtpNgayKT_ValueChanged(object sender, EventArgs e)
        {
            if(dtpNgayBD.Value>dtpNgayKT.Value)
            {
                MessageBox.Show("Lỗi chọn ngày!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                dtpNgayKT.Value = dtpNgayBD.Value = DateTime.Now ;
            }
        }

        private void btnAddQL_Click(object sender, EventArgs e)
        {
            //MaQL,HoTenLot,Ten,GVKhoa,QL_Role,GVKHoa_DB
            if (ValidateQL())
            { 
                dgv_QL.Rows.Add(txtMaQL.Text, txtQLHoTenLot.Text, txtTenQL.Text, cbQL_CoSo.Text,cbQL_Role.Text , cbQL_CoSo.SelectedValue);
                ClearQLFields();
                lblQL_TotalNumber.Text = dgv_QL.RowCount.ToString();
            }
            else return;
        }
        private bool ValidateQL()
        {
            if (txtMaQL.Text.Length == 0)
            {
                MessageBox.Show("Mã Quản Lý Đang Trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            if (FindDupMaQL())
            {
                MessageBox.Show("Mã Quản Lý Đang Bị Trùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
                return true;
        }
        private bool FindDupMaQL()
        {
            foreach (DataGridViewRow row in dgv_QL.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["MaQL"].Value.ToString() == txtMaQL.Text)
                    {
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }
        private void ClearQLFields()
        {
            txtMaQL.Clear();
            txtQLHoTenLot.Clear();
            cbQL_CoSo.SelectedIndex = -1;
            cbQL_CoSo.Text = String.Empty;
            txtTenQL.Clear();
            cbQL_Role.SelectedIndex = -1;
            cbQL_Role.Text = String.Empty;
            txtMaQL.ReadOnly = false;
            btnFindQL.Enabled = true;
        }

        private void btnQLShow_Click(object sender, EventArgs e)
        {
            if (gbHD_QL.Visible)
            {
                gbHD_QL.Visible = false;
                btnQLExport.Text = "Hiện";
            }
            else
            {
                gbHD_QL.Visible = true;
                btnQLExport.Text = "Ẩn";
            }
        }

        private void dgv_QL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgv_QL.CurrentRow;
            if (row != null) //MaQL,HoTenLot,Ten,GVKhoa,QL_Role,GVKHoa_DB
            {
                txtMaQL.Text        = row.Cells["MaQL"].Value.ToString();
                txtMaQL.ReadOnly    = true;
                txtQLHoTenLot.Text  = row.Cells["HoTenLot"].Value.ToString();
                txtTenQL.Text       = row.Cells["Ten"].Value.ToString();
                cbQL_CoSo.SelectedValue = row.Cells["QLCoSo_DB"].Value != null ? row.Cells["QLCoSo_DB"].Value : -1;
                cbQL_Role.Text       = row.Cells["QL_Role"].Value == null ? "" : row.Cells["QL_Role"].Value.ToString();
            }
            else
                return;
        }

        private void btnClearQL_Click(object sender, EventArgs e)
        {
            ClearQLFields();
        }

        private void btnEditQL_Click(object sender, EventArgs e)
        {
            if (txtMaQL.ReadOnly == false || dgv_QL.CurrentRow == null) return;
            dgv_QL.CurrentRow.Cells["HoTenLot"].Value = txtQLHoTenLot.Text;
            dgv_QL.CurrentRow.Cells["Ten"].Value = txtTenQL.Text;
            dgv_QL.CurrentRow.Cells["QLCoSo"].Value = cbQL_CoSo.Text;
            dgv_QL.CurrentRow.Cells["QL_Role"].Value = cbQL_Role.Text;
            dgv_QL.CurrentRow.Cells["QLCoSo_DB"].Value = cbQL_CoSo.SelectedValue;
            ClearQLFields();
        }

        private void btnDelQL_Click(object sender, EventArgs e)
        {
            ClearQLFields();
            if (dgv_QL.Rows.Count == 0 || dgv_QL.CurrentRow.Index < 0) return;
            dgv_QL.Rows.RemoveAt(dgv_QL.CurrentRow.Index);
            lblQL_TotalNumber.Text = dgv_QL.RowCount.ToString();
        }

        private void AddOrUpdateHD_QL(HOAT_DONG hd, Context db)
        {
            foreach (DataGridViewRow dr in dgv_QL.Rows)
            {
                HD_QUANLY HD_QL = db.HD_QUANLY.Find(hd.MaHD, dr.Cells["MaQL"].Value.ToString());
                if (HD_QL == null)
                {
                    HD_QL = new HD_QUANLY();
                    HD_QL.MaHD = hd.MaHD;
                    HD_QL.MaQL = dr.Cells["MaQL"].Value.ToString().Trim();
                    HD_QL.VaiTro = dr.Cells["QL_Role"].Value.ToString().Trim();
                    AddOrUpdateQL(HD_QL, db, dr);
                    hd.HD_QUANLY.Add(HD_QL);
                }
                else
                {
                    HD_QL.VaiTro = dr.Cells["QL_Role"].Value.ToString().Trim();
                    AddOrUpdateQL(HD_QL, db, dr);
                    db.Entry(HD_QL).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        private void AddOrUpdateQL(HD_QUANLY HD_QL, Context db, DataGridViewRow dr)
        {
            if (db.QUAN_LY.Find(HD_QL.MaQL) != null)
            {
                QUAN_LY ql = db.QUAN_LY.Find(HD_QL.MaQL);
                ql.HoTenLot = dr.Cells["HoTenLot"].Value.ToString().Trim();
                ql.Ten = dr.Cells["Ten"].Value.ToString().Trim();
                ql.CoSo = dr.Cells["QLCoSo_DB"].Value.ToString().Trim();
                //ql.VaiTro = dr.Cells["QL_Role"].Value.ToString().Trim();
                ql.Hide = false;
                db.Entry(ql).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                HD_QL.QUAN_LY = new QUAN_LY();
                HD_QL.QUAN_LY.MaQL = HD_QL.MaQL;
                HD_QL.QUAN_LY.HoTenLot = dr.Cells["HoTenLot"].Value.ToString().Trim();
                HD_QL.QUAN_LY.Ten = dr.Cells["Ten"].Value.ToString().Trim();
                HD_QL.QUAN_LY.CoSo = dr.Cells["QLCoSo_DB"].Value.ToString().Trim();
                //HD_QL.QUAN_LY.CoSo = dr.Cells["QL_Role"].Value.ToString().Trim();
                HD_QL.QUAN_LY.Hide = false;
            }
        }

        private void btnFindQL_Click(object sender, EventArgs e)
        {
            //If no value is enter for search, disable button
            if (string.IsNullOrEmpty(txtMaQL.Text.Trim()) && String.IsNullOrEmpty(txtQLHoTenLot.Text.Trim()) && String.IsNullOrEmpty(txtTenQL.Text.Trim()) /*&& String.IsNullOrEmpty(cbQL_CoSo.Text.ToString().Trim())*/)
            {
                btnFindQL.Enabled = false;
                lblQLFind.Text = "";
                return;
            }
            using (Context db = new Context())
            {
                //initialize data
                lblQLFind.Text = "loading...";
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
                if (quanLyData == null)
                {
                    lblQLFind.Text = "";
                    return;
                }
                //filter
                if (!string.IsNullOrEmpty(txtMaQL.Text.Trim()))
                {
                    string maQL = txtMaQL.Text.Trim();
                    quanLyData = (from ql in quanLyData
                                     where ql.MaQL.Contains(maQL)  //thêm && .Hide == false ở đây(và ở những cái dưới) thì sẽ không cần truy vấn đầu nữa (Dự phòng sau này dữ liệu lớn quá)
                                     select ql);
                }
                if (!string.IsNullOrEmpty(txtQLHoTenLot.Text.Trim()))
                {
                    string tenlot = txtQLHoTenLot.Text.Trim();
                    quanLyData = (from ql in quanLyData
                                     where ql.HoTenLot.Contains(tenlot)
                                     select ql);
                }
                if (!string.IsNullOrEmpty(txtTenQL.Text.Trim()))
                {
                    string ten = txtTenQL.Text.Trim();
                    quanLyData = (from ql in quanLyData
                                     where ql.Ten.Contains(ten)
                                     select ql);
                }

                //check again for null
                if (quanLyData == null)
                {
                    lblQLFind.Text = "";
                    return;
                }
                //Insert value
                var QL = quanLyData.FirstOrDefault();
                if (QL == null)
                {
                    lblQLFind.Text = "";
                    return;
                }
                txtMaQL.Text = QL.MaQL.ToString();
                txtQLHoTenLot.Text = QL.HoTenLot.ToString();
                txtTenQL.Text = QL.Ten.ToString();
                txtMaQL.ReadOnly = true;
                txtTenQL.Focus();
                cbQL_CoSo.SelectedValue = QL.CoSo;
                btnFindQL.Enabled = false;
                lblQLFind.Text = "";

                /*QUAN_LY ql = db.QUAN_LY.Find(txtMaQL.Text);
                if (ql != null)
                {
                    txtMaQL.Text = ql.MaQL;
                    txtMaQL.ReadOnly = true;
                    txtQLHoTenLot.Text = ql.HoTenLot;
                    txtTenQL.Text = ql.Ten;
                    cbQL_CoSo.SelectedValue = ql.Khoa;
                    //cbQL_Role.Text = ql.DonVi;
                }
                else
                    return;*/
            }
        }
        private DOI_TAC FindDTByName(string name)
        {
            try
            {
                DOI_TAC result = new DOI_TAC();
                using (Context db = new Context())
                {
                    DOI_TAC dt = (from k in db.DOI_TAC
                                  where (k.Hide == false && k.TenDoiTac.Contains(name))
                                  select k).FirstOrDefault();
                    result = dt;
                }
                return result;
            }
            catch { return null; }
        }

        private void btnDT_Find_Click(object sender, EventArgs e)
        {
            DOI_TAC dt = FindDTByName(txtDT_Ten.Text);
            if (dt != null)
            {
                DT_ID = dt.ID_DoiTac; //Lưu biến tạm để xử lý
                txtDT_Ten.Text = dt.TenDoiTac;
                txtDT_Ten.ReadOnly = true;
                txtDT_Rep.Text = dt.DaiDien;
                txtDT_SDT.Text = dt.SDT;
                txtDT_Email.Text = dt.Email;
            }
            else 
             return; 
        }

        private void btnDT_Clear_Click(object sender, EventArgs e)
        {
            ClearFieldsDT();
        }
        public void ClearFieldsDT()
        {
            txtDT_Ten.ReadOnly = false;
            txtDT_Ten.Clear();
            txtDT_Rep.Clear();
            txtDT_SDT.Clear();
            txtDT_Email.Clear();
            txtDT_NoiDung.Clear();
        }
        //DT_Ten,DT_DaiDien,DT_SDT,DT_Email,DT_NoiDung,ID_DB
        private void btnDT_Add_Click(object sender, EventArgs e)
        {
            if (ValidateDT())
            {
                dgvDoiTac.Rows.Add(txtDT_Ten.Text, txtDT_Rep.Text, txtDT_SDT.Text, txtDT_Email.Text, txtDT_NoiDung.Text,DT_ID);
                DT_ID = -1; //reset DT_ID sau khi sd
                ClearFieldsDT();
            }
            else return;
        }

        private bool ValidateDT()
        {
            if (txtDT_Ten.Text.Length == 0)
            {
                MessageBox.Show("Tên Đối Tác Đang Trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            if (FindDupDT())
            {
                MessageBox.Show("Tên Đối Tác Đang Bị Trùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
                return true;
        }
        private bool FindDupDT()
        {
            foreach (DataGridViewRow row in dgvDoiTac.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["DT_Ten"].Value.ToString() == txtDT_Ten.Text)
                    {
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }

        private void btnDT_Edit_Click(object sender, EventArgs e)
        {
            if (dgvDoiTac.CurrentRow == null) return;
            dgvDoiTac.CurrentRow.Cells["DT_Ten"].Value = txtDT_Ten.Text;
            dgvDoiTac.CurrentRow.Cells["DT_DaiDien"].Value = txtDT_Rep.Text;
            dgvDoiTac.CurrentRow.Cells["DT_SDT"].Value = txtDT_SDT.Text;
            dgvDoiTac.CurrentRow.Cells["DT_Email"].Value = txtDT_Email.Text;
            dgvDoiTac.CurrentRow.Cells["DT_NoiDung"].Value = txtDT_NoiDung.Text;
            ClearFieldsDT();
        }

        private void btnDT_Del_Click(object sender, EventArgs e)
        {
            ClearFieldsDT();
            if (dgvDoiTac.Rows.Count == 0 || dgvDoiTac.CurrentRow.Index < 0) return;
            dgvDoiTac.Rows.RemoveAt(dgvDoiTac.CurrentRow.Index);
        }

        private void dgvDoiTac_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDoiTac.CurrentRow;
            if (row != null) //DT_Ten,DT_DaiDien,DT_SDT,DT_Email,DT_NoiDung,ID_DB
            {
                txtDT_Ten.Text      = row.Cells["DT_Ten"].Value.ToString();
                //txtDT_Ten.ReadOnly  = true;
                txtDT_Rep.Text      = row.Cells["DT_DaiDien"].Value.ToString();
                txtDT_SDT.Text      = row.Cells["DT_SDT"].Value.ToString();
                txtDT_Email.Text    = row.Cells["DT_Email"].Value.ToString();
                txtDT_NoiDung.Text  = row.Cells["DT_NoiDung"].Value.ToString();
            }
            else
                return;
        }

        private TAI_TRO FindTTByName(string name)
        {
            try
            {
                TAI_TRO result = new TAI_TRO();
                using (Context db = new Context())
                {
                    TAI_TRO tt = (from s in db.TAI_TRO
                                  where s.TenTaiTro.Contains(name) && s.Hide == false
                                  select s).FirstOrDefault();
                    result = tt;
                }
                return result;
            }
            catch { return null; }
        }

        private CO_SO FindCoSoByName(string name)
        {
            try
            {
                CO_SO result = new CO_SO();
                using (Context db = new Context())
                {
                    CO_SO tt = (from s in db.CO_SOs
                                where s.TenCoSo.Contains(name) && s.Hide == false
                                  select s).FirstOrDefault();
                    result = tt;
                }
                return result;
            }
            catch { return null; }
        }

        private void btnTT_Find_Click(object sender, EventArgs e)
        {
            TAI_TRO tt = FindTTByName(txtTT_Name.Text);
            if (tt != null)
            {
                TT_ID = tt.ID_TaiTro; //Lưu biến tạm để xử lý
                txtTT_Name.Text = tt.TenTaiTro;
                txtTT_Name.ReadOnly = true;
                txtTT_Rep.Text = tt.DaiDien;
                txtTT_SDT.Text = tt.SDT;
                txtTT_Email.Text = tt.Email;
            }
            else
              return; 
        }

        public void ClearFieldsTT()
        {
            btnTT_Add.Enabled = true;
            txtTT_Name.ReadOnly = false;
            txtTT_Name.Clear();
            txtTT_Rep.Clear();
            txtTT_SDT.Clear();
            txtTT_Email.Clear();
            txtTT_NoiDung.Clear();
        }

        private void btnTT_Clear_Click(object sender, EventArgs e)
        {
            ClearFieldsTT();
        }

        private void btnTT_Add_Click(object sender, EventArgs e)
        {
            if (ValidateTT())
            {
                dgvTaiTro.Rows.Add(txtTT_Name.Text, txtTT_Rep.Text, txtTT_SDT.Text, txtTT_Email.Text, txtTT_NoiDung.Text, TT_ID);
                TT_ID = -1; //reset DT_ID sau khi sd
                ClearFieldsTT();
            }
            else return;
        }
        //TT_Name, TT_Rep, TT_SDT, TT_Email, TT_Notes, TT_IDDB
        private bool ValidateTT()
        {
            if (txtTT_Name.Text.Length == 0)
            {
                MessageBox.Show("Tên Nhà Tài Trợ Đang Trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            if (FindDupTT())
            {
                MessageBox.Show("Tên Nhà Tài Trợ Đang Bị Trùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
                return true;
        }
        private bool FindDupTT()
        {
            foreach (DataGridViewRow row in dgvTaiTro.Rows)
            {
                if (row == null) return false;
                else
                {
                    if (row.Cells["TT_Name"].Value.ToString() == txtTT_Name.Text)
                    {
                        return true;
                    }
                    else continue;
                }
            }
            return false;
        }

        private void btnTT_Edit_Click(object sender, EventArgs e)
        {
            if (dgvTaiTro.CurrentRow == null) return;
            dgvTaiTro.CurrentRow.Cells["TT_Name"].Value = txtTT_Name.Text;
            dgvTaiTro.CurrentRow.Cells["TT_Rep"].Value = txtTT_Rep.Text;
            dgvTaiTro.CurrentRow.Cells["TT_SDT"].Value = txtTT_SDT.Text;
            dgvTaiTro.CurrentRow.Cells["TT_Email"].Value = txtTT_Email.Text;
            dgvTaiTro.CurrentRow.Cells["TT_Notes"].Value = txtTT_NoiDung.Text;
            ClearFieldsTT();
        }

        private void btnTT_Del_Click(object sender, EventArgs e)
        {
            ClearFieldsTT();
            if (dgvTaiTro.Rows.Count == 0 || dgvTaiTro.CurrentRow.Index < 0) return;
            dgvTaiTro.Rows.RemoveAt(dgvTaiTro.CurrentRow.Index);
        }

        private void dgvTaiTro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnTT_Add.Enabled = false;
            DataGridViewRow row = dgvTaiTro.CurrentRow;
            if (row != null) //TT_Name, TT_Rep, TT_SDT, TT_Email, TT_Notes, TT_IDDB
            {
                txtTT_Name.Text = row.Cells["TT_Name"].Value.ToString();
                //txtDT_Ten.ReadOnly  = true;
                txtTT_Rep.Text = row.Cells["TT_Rep"].Value.ToString();
                txtTT_SDT.Text = row.Cells["TT_SDT"].Value.ToString();
                txtTT_Email.Text = row.Cells["TT_Email"].Value.ToString();
                //txtTT_NoiDung.Text = row.Cells["TT_Notes"].Value.ToString();
            }
            else
                return;
        }

        private void btnExportNV_Click(object sender, EventArgs e)
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
                            for (int i = 1; i <= dgvNhanVien.Columns.Count; i++)
                            {
                                worksheet.Cells[1, i].Value = dgvNhanVien.Columns[i - 1].HeaderText;
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgvNhanVien.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgvNhanVien.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 1, j].Value = dgvNhanVien.Rows[i - 1].Cells[j - 1].Value;
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

        private void btnImportNV_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Chọn File nhập";
            fileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportNV(fileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Import không thành công!\n\n" + ex.Message);
                }
            }
            lblNV_TotalNumber.Text = dgvNhanVien.RowCount.ToString();
        }
        private void ImportNV(string path)
        {
            // Đặt license cho EPPlus
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                // Giả sử lấy Sheet đầu tiên
                ExcelWorksheet ws = excelPackage.Workbook.Worksheets[0];

                // Tạo sẵn DataTable với cột cố định theo thứ tự
                DataTable dt = new DataTable();
                dt.Columns.Add("MSNV");
                dt.Columns.Add("Họ và Tên");
                dt.Columns.Add("Mã Cơ Sở");
                dt.Columns.Add("Tên Cơ Sở");
                dt.Columns.Add("Vai Trò");
                dt.Columns.Add("Ghi Chú");

                // Đọc dữ liệu từ hàng 2 trở đi (hàng 1 là header)
                int startRow = 2;
                int endRow = ws.Dimension.End.Row;

                for (int row = startRow; row <= endRow; row++)
                {
                    DataRow dataRow = dt.NewRow();

                    dataRow["MSNV"] = ws.Cells[row, 1].Value?.ToString().Trim() ?? "";
                    dataRow["Họ và Tên"] = ws.Cells[row, 2].Value?.ToString().Trim() ?? "";
                    dataRow["Mã Cơ Sở"] = ws.Cells[row, 3].Value?.ToString().Trim() ?? "";
                    dataRow["Tên Cơ Sở"] = ws.Cells[row, 4].Value?.ToString().Trim() ?? "";
                    dataRow["Vai Trò"] = ws.Cells[row, 5].Value?.ToString().Trim() ?? "";
                    dataRow["Ghi Chú"] = ws.Cells[row, 6].Value?.ToString().Trim() ?? "";

                    dt.Rows.Add(dataRow);
                }

                // Xử lý từng dòng
                foreach (DataRow dr in dt.Rows)
                {
                    // Lấy Tên Cơ Sở trong Excel
                    string tenCS = dr["Tên Cơ Sở"].ToString().Trim();

                    // Thử tìm CO_SO trong DB
                    CO_SO temp = FindCoSoByName(tenCS);
                    if (temp == null)
                    {
                        // Hỏi người dùng có muốn tạo mới?
                        DialogResult d = MessageBox.Show(
                            "Đơn vị " + tenCS + " chưa được tạo, bạn có muốn tạo mới không?\n\n"
                            + "Chú ý: Đơn vị tạo tự động sẽ có ngày thành lập là ngày được tạo",
                            "Chưa tồn tại đơn vị",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Information);

                        if (d == DialogResult.Yes)
                        {
                            // Tạo Mã Cơ Sở từ tên
                            string MaCoSo = string.Join(
                                 string.Empty,
                                 tenCS.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                      .Select(s => s[0])
                            ).ToUpper();

                            temp = new CO_SO()
                            {
                                MaCoSo = MaCoSo,
                                TenCoSo = tenCS,
                                NgayThanhLap = DateTime.Now,
                                Hide = false
                            };

                            // Lưu vào DB
                            try
                            {
                                using (Context db = new Context())
                                {
                                    db.CO_SOs.Add(temp);
                                    db.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                // Hiển thị chi tiết lỗi (kể cả InnerException)
                                MessageBox.Show(
                                    $"Lỗi khi tạo CS mới:\n{ex.GetBaseException().Message}",
                                    "Lỗi",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                );
                                // Bỏ qua dòng này, tránh gây lỗi tiếp
                                continue;
                            }
                        }
                        else
                        {
                            // Người dùng No hoặc Cancel -> bỏ qua hàng
                            continue;
                        }
                    }

                    // Gán ngược "Mã Cơ Sở" đã tìm/đã tạo được
                    dr["Mã Cơ Sở"] = temp.MaCoSo;

                    // Kiểm tra xem trong dgv đã có MSNV này chưa?
                    // Nếu UpdateDgv_NV() trả về true, nghĩa là có trùng => ta cập nhật
                    // Còn nếu false => ta thêm mới
                    if (UpdateDgv_NV(dr))
                    {
                        // Cập nhật xong -> next
                        continue;
                    }

                    // Nếu chưa có thì thêm hẳn 1 dòng mới vào dgv
                    dgvNhanVien.Rows.Add(dr.ItemArray);
                }
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
                                worksheet.Cells[1, i].Value = dgv_QL.Columns[i - 1].HeaderText;
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgv_QL.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgv_QL.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 1, j].Value = dgv_QL.Rows[i - 1].Cells[j - 1].Value;
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

        private void btnQLImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Chọn File nhập";
            fileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportQL(fileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Import không thành công!\n\n" + ex.Message);
                }
            }
            lblQL_TotalNumber.Text = dgv_QL.RowCount.ToString();
        }

        private void ImportQL(string path)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                DataTable dt = new DataTable();
                for (int i = excelWorksheet.Dimension.Start.Column; i <= excelWorksheet.Dimension.End.Column; i++)
                {
                    dt.Columns.Add(excelWorksheet.Cells[1, i].Value.ToString().Trim());
                }
                for (int i = excelWorksheet.Dimension.Start.Row + 1; i <= excelWorksheet.Dimension.End.Row; i++)
                {
                    List<string> listRow = new List<string>();
                    for (int j = excelWorksheet.Dimension.Start.Column; j <= excelWorksheet.Dimension.End.Column; j++)
                    {
                        if (excelWorksheet.Cells[i, j].Value == null)
                        {
                            listRow.Add("");
                            continue;
                        }
                        listRow.Add(excelWorksheet.Cells[i, j].Value.ToString().Trim());
                    }
                    dt.Rows.Add(listRow.ToArray());
                }
                foreach (DataRow dr in dt.Rows)
                {
                    //fill mã cs
                    string nameTemp = dr[3].ToString().Trim();
                    CO_SO temp = FindCoSoByName(nameTemp);
                    if (temp == null)
                    {
                        DialogResult d = MessageBox.Show("Đơn vị " + nameTemp + " chưa được tạo, bạn có muốn tạo mới không?\n Chú ý: Đơn vị tạo tự động sẽ có ngày thành lập là ngày được tạo", "Chưa tồn tại đơn vị", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                        if (d == DialogResult.Yes)
                        {
                            //Tao maCoSo tu dong
                            string MaCoSo = string.Join(string.Empty, nameTemp.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s[0])).ToUpper();
                            temp = new CO_SO();
                            temp.MaCoSo = MaCoSo;
                            temp.TenCoSo = nameTemp;
                            temp.NgayThanhLap = DateTime.Now;
                            temp.Hide = false;
                            try
                            {
                                using (Context db = new Context())
                                {
                                    db.CO_SOs.Add(temp);
                                    db.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi khi tạo cs mới, vui lòng liên hệ admin\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else continue;
                    }
                    dr[5] = temp.MaCoSo;
                    //so từng hàng trong file excel với bảng dgv hiện tại, nếu trùng, cập nhật, không trùng, thêm vào dgv
                    if (Updatedgv_QL(dr)) continue;
                    dgv_QL.Rows.Add(dr.ItemArray);

                }
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
                        FileInfo excelFile = new FileInfo(saveFileDialog.FileName); ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        using (ExcelPackage excelPackage = new ExcelPackage())
                        {
                            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(txtTenHD.Text);

                            // Ghi header của DataGridView vào Excel
                            for (int i = 1; i <= dgvDoiTac.Columns.Count; i++)
                            {
                                worksheet.Cells[1, i].Value = dgvDoiTac.Columns[i - 1].HeaderText;
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgvDoiTac.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgvDoiTac.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 1, j].Value = dgvDoiTac.Rows[i - 1].Cells[j - 1].Value;
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

        private void btnDTImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Chọn File nhập";
            fileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImportDT(fileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Import không thành công!\n\n" + ex.Message);
                }
            }
        }

        private void ImportDT(string path)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                DataTable dt = new DataTable();
                for (int i = excelWorksheet.Dimension.Start.Column; i <= excelWorksheet.Dimension.End.Column; i++)
                {
                    dt.Columns.Add(excelWorksheet.Cells[1, i].Value.ToString().Trim());
                }
                for (int i = excelWorksheet.Dimension.Start.Row + 1; i <= excelWorksheet.Dimension.End.Row; i++)
                {
                    List<string> listRow = new List<string>();
                    for (int j = excelWorksheet.Dimension.Start.Column; j <= excelWorksheet.Dimension.End.Column; j++)
                    {
                        if (excelWorksheet.Cells[i, j].Value == null)
                        {
                            listRow.Add("");
                            continue;
                        }
                        listRow.Add(excelWorksheet.Cells[i, j].Value.ToString().Trim());
                    }
                    dt.Rows.Add(listRow.ToArray());
                }
                foreach (DataRow dr in dt.Rows)
                {
                    if (FindDuplicateDT(dr)) continue;
                    dgvDoiTac.Rows.Add(dr.ItemArray);

                }
            }
        }

        private void btnTTExport_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
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
                                worksheet.Cells[1, i].Value = dgvTaiTro.Columns[i - 1].HeaderText;
                                worksheet.Cells[1, i].Style.Font.Bold = true;
                            }

                            // Ghi dữ liệu từ DataGridView vào Excel
                            for (int i = 1; i <= dgvTaiTro.Rows.Count; i++)
                            {
                                for (int j = 1; j <= dgvTaiTro.Columns.Count; j++)
                                {
                                    worksheet.Cells[i + 1, j].Value = dgvTaiTro.Rows[i - 1].Cells[j - 1].Value;
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

        private void btnTTImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Chọn File nhập";
            fileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    ImportTT(fileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Import không thành công!\n\n" + ex.Message);
                }
            }
        }

        private void ImportTT(string path)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                DataTable dt = new DataTable();
                for (int i = excelWorksheet.Dimension.Start.Column; i <= excelWorksheet.Dimension.End.Column; i++)
                {
                    dt.Columns.Add(excelWorksheet.Cells[1, i].Value.ToString().Trim());
                }
                for (int i = excelWorksheet.Dimension.Start.Row + 1; i <= excelWorksheet.Dimension.End.Row; i++)
                {
                    List<string> listRow = new List<string>();
                    for (int j = excelWorksheet.Dimension.Start.Column; j <= excelWorksheet.Dimension.End.Column; j++)
                    {
                        if (excelWorksheet.Cells[i, j].Value == null)
                        {
                            listRow.Add("");
                            continue;
                        }
                        listRow.Add(excelWorksheet.Cells[i, j].Value.ToString().Trim());
                    }
                    dt.Rows.Add(listRow.ToArray());
                }
                foreach (DataRow dr in dt.Rows)
                {
                    if (FindDuplicateTT(dr)) continue;
                    dgvTaiTro.Rows.Add(dr.ItemArray);

                }
            }
        }

        private void label34_Click(object sender, EventArgs e)
        {
            lblNV_TotalNumber.Text = dgvNhanVien.RowCount.ToString();
        }

        private void label35_Click(object sender, EventArgs e)
        {
            lblQL_TotalNumber.Text = dgv_QL.RowCount.ToString();
        }

        private void DeleteAllMembers()
        {
            try 
            {
                using (Context db = new Context())
                {
                    HOAT_DONG hD = db.HOAT_DONG.Find(id);
                    List<HD_NHAN_VIEN> NVList = hD.HD_NHAN_VIEN.ToList();
                    foreach (HD_NHAN_VIEN NV in NVList)
                    {
                        db.HD_NHAN_VIEN.Remove(NV);
                    }

                    List<HD_QUANLY> List = hD.HD_QUANLY.ToList();
                    foreach (HD_QUANLY QL in List)
                    {
                        db.HD_QUANLY.Remove(QL);
                    }


                    List<HD_DOITAC> DTList = hD.HD_DOITAC.ToList();
                    foreach (HD_DOITAC DT in DTList)
                    {
                        db.HD_DOITAC.Remove(DT);
                    }
                    List<HD_TAITRO> TTList = hD.HD_TAITRO.ToList();
                    foreach (HD_TAITRO TT in TTList)
                    {
                        db.HD_TAITRO.Remove(TT);
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("error while DeleteAllMembers() \n\n" + ex.Message);
            }
        }

        private void txtMSNV_TextChanged(object sender, EventArgs e)
        {
            btnNVFind.Enabled = true;
        }

        private void txtNVHoTen_TextChanged(object sender, EventArgs e)
        {
            btnNVFind.Enabled = true;
        }

        private void cbCoSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnNVFind.Enabled = true;
        }

        private void txtMaQL_TextChanged(object sender, EventArgs e)
        {
            btnFindQL.Enabled = true;
        }

        private void txtQLHoTenLot_TextChanged(object sender, EventArgs e)
        {
            btnFindQL.Enabled = true;
        }

        private void txtTenQL_TextChanged(object sender, EventArgs e)
        {
            btnFindQL.Enabled = true;
        }

        private void cbQL_CoSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFindQL.Enabled = true;
        }

        private void gbGeneralInfo_Enter(object sender, EventArgs e)
        {

        }

        private void txtDT_SDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void txtTT_SDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_QL_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
