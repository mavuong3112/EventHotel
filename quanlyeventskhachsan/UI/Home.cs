using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanlyeventskhachsan.MODEL;
namespace quanlyeventskhachsan.UI
{
    public partial class frmHome : Form
    {

        public frmHome()
        {
            InitializeComponent();
        }
        private void container(object _form)
        {
            if (panel_Feedback.Controls.Count > 0)
            {
                foreach (Control ctrl in panel_Feedback.Controls)
                {
                    ctrl.Dispose();
                }
                panel_Feedback.Controls.Clear();
            }

            //if (panel_Feedback.Controls.Count > 0)
            //{
            //    panel_Feedback.Controls.Clear();
            //}
            Form frm = _form as Form;
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            panel_Feedback.Controls.Add(frm);
            panel_Feedback.Tag = frm;
            frm.Show();
        }
        private void btn_HoatDong(object sender, EventArgs e)
        {
            container(new frm_Theme());
        }

        private void btn_DoiTac(object sender, EventArgs e)
        {
            container(new frm_DoiTac_Details());
        }

        private void panel_Menu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_NhanVien(object sender, EventArgs e)
        {
            container(new frmNhanVien());
        }

        private void btn_HangMuc(object sender, EventArgs e)
        {
            container(new frmAddHangMuc());

        }

        private void btn_CoSo(object sender, EventArgs e)
        {
            container(new frm_CoSo_Details());
        }

        private void btn_QuanLy(object sender, EventArgs e)
        {
            container(new frm_QuanLy());
        }

        private void panel_Feedback_Paint(object sender, PaintEventArgs e)
        {

        }


        private void btn_TTQL(object sender, EventArgs e)
        {
            container(new frmTK_QuanLy());
        }

        private void btn_TTDT(object sender, EventArgs e)
        {
            container(new frmTK_DoiTac());
        }

        private void btn_TTTT(object sender, EventArgs e)
        {
            container(new frmTK_TaiTro());
        }

        private void btn_TKTC(object sender, EventArgs e)
        {
            container(new frmTK_TaiChinh());
        }

        private void btn_TKCoSo(object sender, EventArgs e)
        {
            container(new frmTK_NhanVien());
        }

        private void btn_TaiTro(object sender, EventArgs e)
        {
            container(new frmTTDetails());
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            container(new frmTK_CoSo());
        }

        private void Home_Load(object sender, EventArgs e)
        {
            container(new frm_Theme());
        }

        private void guna2PictureBox1_Click_1(object sender, EventArgs e)
        {
            container(new frm_Theme());

        }
    }
}
