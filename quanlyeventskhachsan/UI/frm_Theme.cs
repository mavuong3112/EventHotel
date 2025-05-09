﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using quanlyeventskhachsan.MODEL;

namespace quanlyeventskhachsan.UI
{
    public partial class frm_Theme : Form
    {
        public frm_Theme()
        {
            InitializeComponent();
        }
        Context db = new Context();
        public void LoadLoai()
        {
            cmbLoai.Items.Add("Sinh nhật");
            cmbLoai.Items.Add("Sự kiện");
            cmbLoai.Items.Add("Đám cưới");
        }
        public void LoadLoai(string loai)
        {

            var lst = from s in db.HOAT_DONG
                      where s.Loai == loai && s.Hide == false
                      select new
                      {
                          MaHD = s.MaHD,
                          TenHoatDong = s.TenHoatDong,
                          Loai = s.Loai,
                          NgayBatDau = s.NgayBatDau,
                          NgayKetThuc = s.NgayKetThuc,
                          CreatedDate = s.CreatedDate
                      };
            foreach (var hd in lst)
            {
                dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
            }

        }
        private void frm_Theme_Load(object sender, EventArgs e)
        {
            // Gọi hàm để load dữ liệu hoạt động vào DataGridView
            LoadLoai();
            LoadDataToDGV_HoatDong();
            btnLoc.Enabled = false;
        }

        private void LoadDataToDGV_HoatDong()
        {
            dgv_HoatDong.Rows.Clear();
            dgv_HoatDong.Refresh();
            using (Context dbContext = new Context())
            {
                // Truy vấn LINQ để lấy dữ liệu từ bảng HOAT_DONG
                var hoatDongData = (from hoatDong in dbContext.HOAT_DONG
                                    where hoatDong.Hide == false
                                    orderby hoatDong.CreatedDate descending
                                    select new
                                    {
                                        MaHD = hoatDong.MaHD,
                                        TenHoatDong = hoatDong.TenHoatDong,
                                        Loai = hoatDong.Loai,
                                        NgayBatDau = hoatDong.NgayBatDau,
                                        NgayKetThuc = hoatDong.NgayKetThuc,
                                        CreatedDate = hoatDong.CreatedDate
                                    });  //lấy 300 hoạt động đầu cho khỏi lag

                //// Gán dữ liệu cho DataGridView dgv_HoatDong
                foreach (var hd in hoatDongData)
                {
                    dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                }
            }
        }

        private void view_HoatDong(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng được chọn không
            if (dgv_HoatDong.SelectedRows.Count > 0)
            {
                // Lấy giá trị MaHD từ dòng được chọn
                int maHD = (int)dgv_HoatDong.SelectedRows[0].Cells["MaHD"].Value;

                // Tạo instance của frmViewHoatDong và truyền giá trị MaHD
                frmViewHoatDong frmView = new frmViewHoatDong();
                frmView.LoadFormView(maHD);

                // Mở form frmViewHoatDong
                frmView.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hoạt động để xem.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_XoaHD(object sender, EventArgs e)
        {
            // Thêm try-catch để xử lý lỗi
            try
            {
                // Kiểm tra xem có dòng được chọn không
                if (dgv_HoatDong.SelectedRows.Count > 0)
                {
                    // Lấy giá trị MaHD từ dòng được chọn
                    string tenHD = dgv_HoatDong.SelectedRows[0].Cells["TenHoatDong"].Value.ToString();
                    int maHD = (int)dgv_HoatDong.SelectedRows[0].Cells["MaHD"].Value;

                    // Hiển thị hộp thoại xác nhận
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa {tenHD} không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // Xác nhận xóa nếu người dùng nhấn Yes
                    if (result == DialogResult.Yes)
                    {
                        // Thực hiện xóa hoạt động ở đây
                        DeleteHoatDong(maHD);
                        // Cập nhật lại dgv_HoatDong sau khi xóa
                        LoadDataToDGV_HoatDong();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một hoạt động để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // In ra thông điệp lỗi chi tiết ra console hoặc ghi vào log
                Console.WriteLine(ex.Message);
                MessageBox.Show("Đã xảy ra lỗi khi xóa hoạt động.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DeleteHoatDong(int maHD)
        {
            using (Context dbContext = new Context()) // Sử dụng một đối tượng Context mới
            {
                // Tìm hoạt động cần xóa dựa trên MaHD
                HOAT_DONG hoatDongToDelete = dbContext.HOAT_DONG.FirstOrDefault(hd => hd.MaHD == maHD);

                if (hoatDongToDelete != null)
                {
                    // Xóa hoạt động từ DbSet và lưu vào cơ sở dữ liệu
                    hoatDongToDelete.Hide = true;
                    dbContext.Entry(hoatDongToDelete).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (dgv_HoatDong.SelectedRows.Count > 0)
            {
                int EventID = (int)dgv_HoatDong.CurrentRow.Cells["MaHD"].Value;
                frmAddHoatDong Update = new frmAddHoatDong();
                Update.LoadFormUpdate(EventID);
                Update.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hoạt động để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_AddHD(object sender, EventArgs e)
        {
            frmAddHoatDong fm = new frmAddHoatDong();
            fm.ShowDialog();
        }
        private void LoadTen_Loai(string ten, string loai)
        {
            var lst = from s in db.HOAT_DONG
                      where s.TenHoatDong.Contains(ten) && s.Loai == loai
                      select new
                      {
                          MaHD = s.MaHD,
                          TenHoatDong = s.TenHoatDong,
                          Loai = s.Loai,
                          NgayBatDau = s.NgayBatDau,
                          NgayKetThuc = s.NgayKetThuc,
                          CreatedDate = s.CreatedDate
                      };
            foreach (var hd in lst)
            {
                dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
            }
        }
        

        private void btnLoc_Click_1(object sender, EventArgs e)
        {
            dgv_HoatDong.Rows.Clear();
            dgv_HoatDong.Refresh();
            if (cmbLoai.SelectedIndex != -1)
            {
                if (string.IsNullOrEmpty(txtSearch.Text) == true && dtpNgayBD.Text == " " && dtpNgayKT.Text == " ")
                {
                    string loai = cmbLoai.SelectedItem.ToString();
                    LoadLoai(loai);
                }
                else if (string.IsNullOrEmpty(txtSearch.Text) != true && dtpNgayBD.Text == " " && dtpNgayKT.Text == " ")
                {
                    string ten = txtSearch.Text;
                    string loai = cmbLoai.SelectedItem.ToString();
                    LoadTen_Loai(ten, loai);
                }
                else if (string.IsNullOrEmpty(txtSearch.Text) == true && dtpNgayBD.Text != " " && dtpNgayKT.Text == " ")
                {
                    string loai = cmbLoai.SelectedItem.ToString();
                    DateTime BD = DateTime.ParseExact(dtpNgayBD.Text, "dd-MM-yyyy", null);
                    var lst = from s in db.HOAT_DONG
                              where  s.Loai == loai && s.NgayBatDau>=BD
                              select new
                              {
                                  MaHD = s.MaHD,
                                  TenHoatDong = s.TenHoatDong,
                                  Loai = s.Loai,
                                  NgayBatDau = s.NgayBatDau,
                                  NgayKetThuc = s.NgayKetThuc,
                                  CreatedDate = s.CreatedDate
                              };
                    foreach (var hd in lst)
                    {
                        dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                    }
                }
                else if (string.IsNullOrEmpty(txtSearch.Text) == true && dtpNgayBD.Text == " " && dtpNgayKT.Text != " ")
                {
                    string loai = cmbLoai.SelectedItem.ToString();
                    DateTime KT = DateTime.ParseExact(dtpNgayKT.Text, "dd-MM-yyyy", null);
                    var lst = from s in db.HOAT_DONG
                              where s.Loai == loai && s.NgayKetThuc <=KT
                              select new
                              {
                                  MaHD = s.MaHD,
                                  TenHoatDong = s.TenHoatDong,
                                  Loai = s.Loai,
                                  NgayBatDau = s.NgayBatDau,
                                  NgayKetThuc = s.NgayKetThuc,
                                  CreatedDate = s.CreatedDate
                              };
                    foreach (var hd in lst)
                    {
                        dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                    }
                }    
                else if (string.IsNullOrEmpty(txtSearch.Text) != true && dtpNgayBD.Text != " " && dtpNgayKT.Text == " ")
                {
                    string ten = txtSearch.Text;
                    string loai = cmbLoai.SelectedItem.ToString();
                    DateTime BD = DateTime.ParseExact(dtpNgayBD.Text, "dd-MM-yyyy", null);
                    var lst = from s in db.HOAT_DONG
                              where s.Loai == loai && s.NgayBatDau >= BD && s.TenHoatDong.Contains(ten)
                              select new
                              {
                                  MaHD = s.MaHD,
                                  TenHoatDong = s.TenHoatDong,
                                  Loai = s.Loai,
                                  NgayBatDau = s.NgayBatDau,
                                  NgayKetThuc = s.NgayKetThuc,
                                  CreatedDate = s.CreatedDate
                              };
                    foreach (var hd in lst)
                    {
                        dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                    }
                }  
                else if (string.IsNullOrEmpty(txtSearch.Text) != true && dtpNgayBD.Text == " " && dtpNgayKT.Text != " ")
                {
                    string ten = txtSearch.Text;
                    string loai = cmbLoai.SelectedItem.ToString();
                    DateTime KT = DateTime.ParseExact(dtpNgayKT.Text, "dd-MM-yyyy", null);
                    var lst = from s in db.HOAT_DONG
                              where s.Loai == loai && s.NgayKetThuc <= KT && s.TenHoatDong.Contains(ten)
                              select new
                              {
                                  MaHD = s.MaHD,
                                  TenHoatDong = s.TenHoatDong,
                                  Loai = s.Loai,
                                  NgayBatDau = s.NgayBatDau,
                                  NgayKetThuc = s.NgayKetThuc,
                                  CreatedDate = s.CreatedDate
                              };
                    foreach (var hd in lst)
                    {
                        dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                    }
                }   
                else if (string.IsNullOrEmpty(txtSearch.Text) == true && dtpNgayBD.Text != " " && dtpNgayKT.Text != " ")
                {
                    string loai = cmbLoai.SelectedItem.ToString();
                    DateTime BD = DateTime.ParseExact(dtpNgayBD.Text, "dd-MM-yyyy", null);
                    DateTime KT = DateTime.ParseExact(dtpNgayKT.Text, "dd-MM-yyyy", null);
                    var lst = from s in db.HOAT_DONG
                              where s.Loai == loai && s.NgayBatDau >= BD && s.NgayKetThuc <= KT 
                              select new
                              {
                                  MaHD = s.MaHD,
                                  TenHoatDong = s.TenHoatDong,
                                  Loai = s.Loai,
                                  NgayBatDau = s.NgayBatDau,
                                  NgayKetThuc = s.NgayKetThuc,
                                  CreatedDate = s.CreatedDate
                              };
                    foreach (var hd in lst)
                    {
                        dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                    }
                }    
                else if (string.IsNullOrEmpty(txtSearch.Text) != true && dtpNgayBD.Text != " " && dtpNgayKT.Text != " ")
                {
                    string ten = txtSearch.Text;
                    string loai = cmbLoai.SelectedItem.ToString();
                    DateTime BD = DateTime.ParseExact(dtpNgayBD.Text, "dd-MM-yyyy", null);
                    DateTime KT = DateTime.ParseExact(dtpNgayKT.Text, "dd-MM-yyyy", null);
                    var lst = from s in db.HOAT_DONG
                              where s.Loai == loai && s.NgayBatDau >= BD && s.NgayKetThuc <= KT && s.TenHoatDong.Contains(ten)
                              select new
                              {
                                  MaHD = s.MaHD,
                                  TenHoatDong = s.TenHoatDong,
                                  Loai = s.Loai,
                                  NgayBatDau = s.NgayBatDau,
                                  NgayKetThuc = s.NgayKetThuc,
                                  CreatedDate = s.CreatedDate
                              };
                    foreach (var hd in lst)
                    {
                        dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                    }
                }    
            }
            else
            {
                if (string.IsNullOrEmpty(txtSearch.Text) == true && dtpNgayBD.Text == " " && dtpNgayKT.Text == " ")
                {
                    LoadDataToDGV_HoatDong();
                }
                else if (string.IsNullOrEmpty(txtSearch.Text) != true && dtpNgayBD.Text == " " && dtpNgayKT.Text == " ")
                {
                    string ten = txtSearch.Text;
                    var lst = from s in db.HOAT_DONG
                              where s.TenHoatDong.Contains(ten)
                              select new
                              {
                                  MaHD = s.MaHD,
                                  TenHoatDong = s.TenHoatDong,
                                  Loai = s.Loai,
                                  NgayBatDau = s.NgayBatDau,
                                  NgayKetThuc = s.NgayKetThuc,
                                  CreatedDate = s.CreatedDate
                              };
                    foreach (var hd in lst)
                    {
                        dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                    }
                }
                else if (string.IsNullOrEmpty(txtSearch.Text) == true && dtpNgayBD.Text != " " && dtpNgayKT.Text == " ")
                {
                    DateTime BD = DateTime.ParseExact(dtpNgayBD.Text, "dd-MM-yyyy", null);
                    var lst = from s in db.HOAT_DONG
                              where  s.NgayBatDau >= BD
                              select new
                              {
                                  MaHD = s.MaHD,
                                  TenHoatDong = s.TenHoatDong,
                                  Loai = s.Loai,
                                  NgayBatDau = s.NgayBatDau,
                                  NgayKetThuc = s.NgayKetThuc,
                                  CreatedDate = s.CreatedDate
                              };
                    foreach (var hd in lst)
                    {
                        dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                    }
                }
                else if (string.IsNullOrEmpty(txtSearch.Text) == true && dtpNgayBD.Text == " " && dtpNgayKT.Text != " ")
                {
                    DateTime KT = DateTime.ParseExact(dtpNgayKT.Text, "dd-MM-yyyy", null);
                    var lst = from s in db.HOAT_DONG
                              where  s.NgayKetThuc <= KT
                              select new
                              {
                                  MaHD = s.MaHD,
                                  TenHoatDong = s.TenHoatDong,
                                  Loai = s.Loai,
                                  NgayBatDau = s.NgayBatDau,
                                  NgayKetThuc = s.NgayKetThuc,
                                  CreatedDate = s.CreatedDate
                              };
                    foreach (var hd in lst)
                    {
                        dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                    }
                }
                else if (string.IsNullOrEmpty(txtSearch.Text) != true && dtpNgayBD.Text != " " && dtpNgayKT.Text == " ")
                {
                    string ten = txtSearch.Text;
                    DateTime BD = DateTime.ParseExact(dtpNgayBD.Text, "dd-MM-yyyy", null);
                    var lst = from s in db.HOAT_DONG
                              where s.NgayBatDau >= BD && s.TenHoatDong.Contains(ten)
                              select new
                              {
                                  MaHD = s.MaHD,
                                  TenHoatDong = s.TenHoatDong,
                                  Loai = s.Loai,
                                  NgayBatDau = s.NgayBatDau,
                                  NgayKetThuc = s.NgayKetThuc,
                                  CreatedDate = s.CreatedDate
                              };
                    foreach (var hd in lst)
                    {
                        dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                    }
                }
                else if (string.IsNullOrEmpty(txtSearch.Text) != true && dtpNgayBD.Text == " " && dtpNgayKT.Text != " ")
                {
                    string ten = txtSearch.Text;
                    DateTime KT = DateTime.ParseExact(dtpNgayKT.Text, "dd-MM-yyyy", null);
                    var lst = from s in db.HOAT_DONG
                              where s.NgayKetThuc <= KT && s.TenHoatDong.Contains(ten)
                              select new
                              {
                                  MaHD = s.MaHD,
                                  TenHoatDong = s.TenHoatDong,
                                  Loai = s.Loai,
                                  NgayBatDau = s.NgayBatDau,
                                  NgayKetThuc = s.NgayKetThuc,
                                  CreatedDate = s.CreatedDate
                              };
                    foreach (var hd in lst)
                    {
                        dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                    }
                }
                else if (string.IsNullOrEmpty(txtSearch.Text) == true && dtpNgayBD.Text != " " && dtpNgayKT.Text != " ")
                {
                    DateTime BD = DateTime.ParseExact(dtpNgayBD.Text, "dd-MM-yyyy", null);
                    DateTime KT = DateTime.ParseExact(dtpNgayKT.Text, "dd-MM-yyyy", null);
                    var lst = from s in db.HOAT_DONG
                              where s.NgayBatDau >= BD && s.NgayKetThuc <= KT
                              select new
                              {
                                  MaHD = s.MaHD,
                                  TenHoatDong = s.TenHoatDong,
                                  Loai = s.Loai,
                                  NgayBatDau = s.NgayBatDau,
                                  NgayKetThuc = s.NgayKetThuc,
                                  CreatedDate = s.CreatedDate
                              };
                    foreach (var hd in lst)
                    {
                        dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                    }
                }
                else if (string.IsNullOrEmpty(txtSearch.Text) != true && dtpNgayBD.Text != " " && dtpNgayKT.Text != " ")
                {
                    string ten = txtSearch.Text;
                    DateTime BD = DateTime.ParseExact(dtpNgayBD.Text, "dd-MM-yyyy", null);
                    DateTime KT = DateTime.ParseExact(dtpNgayKT.Text, "dd-MM-yyyy", null);
                    var lst = from s in db.HOAT_DONG
                              where  s.NgayBatDau >= BD && s.NgayKetThuc <= KT && s.TenHoatDong.Contains(ten)
                              select new
                              {
                                  MaHD = s.MaHD,
                                  TenHoatDong = s.TenHoatDong,
                                  Loai = s.Loai,
                                  NgayBatDau = s.NgayBatDau,
                                  NgayKetThuc = s.NgayKetThuc,
                                  CreatedDate = s.CreatedDate
                              };
                    foreach (var hd in lst)
                    {
                        dgv_HoatDong.Rows.Add(hd.MaHD, hd.TenHoatDong, hd.Loai, hd.NgayBatDau, hd.NgayKetThuc, hd.CreatedDate);
                    }
                }
            }    
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }
        private void cmbLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnLoc.Enabled = true;
        }
        private void dtpNgayBD_ValueChanged(object sender, EventArgs e)
        {
            dtpNgayBD.CustomFormat = "dd-MM-yyyy";
            btnLoc.Enabled = true;
        }

        private void dtpNgayBD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                dtpNgayBD.CustomFormat = " ";
            }
        }

        private void dtpNgayKT_ValueChanged(object sender, EventArgs e)
        {
            dtpNgayKT.CustomFormat = "dd-MM-yyyy";
            btnLoc.Enabled = true;
        }

        private void dtpNgayKT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                dtpNgayKT.CustomFormat = " ";
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            dgv_HoatDong.Rows.Clear();
            dgv_HoatDong.Refresh();
            txtSearch.Text = "";
            cmbLoai.SelectedIndex = -1;
            dtpNgayBD.CustomFormat = " ";
            dtpNgayKT.CustomFormat = " ";
            LoadDataToDGV_HoatDong();
            btnLoc.Enabled = false;
        }

        private void dgv_HoatDong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
