namespace quanlyeventskhachsan.UI
{
    partial class frmViewHoatDong
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.gbGeneralInfo = new System.Windows.Forms.GroupBox();
            this.txtDateEnd = new System.Windows.Forms.TextBox();
            this.txtDateBegin = new System.Windows.Forms.TextBox();
            this.txtLoai = new System.Windows.Forms.TextBox();
            this.txtTenHD = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbNVList = new System.Windows.Forms.GroupBox();
            this.dgvNhanVien = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.MSNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoTenNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notes_NV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DB_CoSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblQL_TotalNumber = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lblNV_TotalNumber = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.btnTTExport = new System.Windows.Forms.Button();
            this.btnDTExport = new System.Windows.Forms.Button();
            this.btnQLExport = new System.Windows.Forms.Button();
            this.btnNVExport = new System.Windows.Forms.Button();
            this.gbTaiChinh = new System.Windows.Forms.GroupBox();
            this.numUEF = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.numTaiTro = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.txtTC_Khac = new System.Windows.Forms.TextBox();
            this.txtTC_TieuDe = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.gbTTList = new System.Windows.Forms.GroupBox();
            this.dgvTaiTro = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.TT_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TT_Rep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TT_SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TT_Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TT_Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TT_IDDB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbDT_List = new System.Windows.Forms.GroupBox();
            this.dgvDoiTac = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.DT_Ten = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_DaiDien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_SDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_NoiDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_DB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbQLList = new System.Windows.Forms.GroupBox();
            this.dgv_QL = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.MaQL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoTenLot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ten = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QLCoSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QL_Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QLCS_DB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbGeneralInfo.SuspendLayout();
            this.gbNVList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            this.panel1.SuspendLayout();
            this.gbTaiChinh.SuspendLayout();
            this.gbTTList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiTro)).BeginInit();
            this.gbDT_List.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoiTac)).BeginInit();
            this.gbQLList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QL)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Blue;
            this.lblHeader.Location = new System.Drawing.Point(394, 15);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(345, 37);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Thông Tin Hoạt Động";
            // 
            // gbGeneralInfo
            // 
            this.gbGeneralInfo.Controls.Add(this.txtDateEnd);
            this.gbGeneralInfo.Controls.Add(this.txtDateBegin);
            this.gbGeneralInfo.Controls.Add(this.txtLoai);
            this.gbGeneralInfo.Controls.Add(this.txtTenHD);
            this.gbGeneralInfo.Controls.Add(this.label3);
            this.gbGeneralInfo.Controls.Add(this.label5);
            this.gbGeneralInfo.Controls.Add(this.label4);
            this.gbGeneralInfo.Controls.Add(this.label2);
            this.gbGeneralInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gbGeneralInfo.Location = new System.Drawing.Point(14, 12);
            this.gbGeneralInfo.Margin = new System.Windows.Forms.Padding(2);
            this.gbGeneralInfo.Name = "gbGeneralInfo";
            this.gbGeneralInfo.Padding = new System.Windows.Forms.Padding(2);
            this.gbGeneralInfo.Size = new System.Drawing.Size(951, 205);
            this.gbGeneralInfo.TabIndex = 1;
            this.gbGeneralInfo.TabStop = false;
            this.gbGeneralInfo.Text = "Thông tin cơ bản";
            // 
            // txtDateEnd
            // 
            this.txtDateEnd.Location = new System.Drawing.Point(562, 154);
            this.txtDateEnd.Margin = new System.Windows.Forms.Padding(2);
            this.txtDateEnd.Name = "txtDateEnd";
            this.txtDateEnd.ReadOnly = true;
            this.txtDateEnd.Size = new System.Drawing.Size(140, 26);
            this.txtDateEnd.TabIndex = 4;
            // 
            // txtDateBegin
            // 
            this.txtDateBegin.Location = new System.Drawing.Point(156, 155);
            this.txtDateBegin.Margin = new System.Windows.Forms.Padding(2);
            this.txtDateBegin.Name = "txtDateBegin";
            this.txtDateBegin.ReadOnly = true;
            this.txtDateBegin.Size = new System.Drawing.Size(140, 26);
            this.txtDateBegin.TabIndex = 4;
            // 
            // txtLoai
            // 
            this.txtLoai.Location = new System.Drawing.Point(156, 106);
            this.txtLoai.Margin = new System.Windows.Forms.Padding(2);
            this.txtLoai.Name = "txtLoai";
            this.txtLoai.ReadOnly = true;
            this.txtLoai.Size = new System.Drawing.Size(230, 26);
            this.txtLoai.TabIndex = 4;
            // 
            // txtTenHD
            // 
            this.txtTenHD.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenHD.Location = new System.Drawing.Point(156, 38);
            this.txtTenHD.Margin = new System.Windows.Forms.Padding(2);
            this.txtTenHD.Name = "txtTenHD";
            this.txtTenHD.ReadOnly = true;
            this.txtTenHD.Size = new System.Drawing.Size(790, 33);
            this.txtTenHD.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 106);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Loại";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(448, 158);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Ngày kết thúc";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 158);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Ngày bắt đầu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên Hoạt Động";
            // 
            // gbNVList
            // 
            this.gbNVList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNVList.Controls.Add(this.dgvNhanVien);
            this.gbNVList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbNVList.Location = new System.Drawing.Point(14, 275);
            this.gbNVList.Margin = new System.Windows.Forms.Padding(2);
            this.gbNVList.Name = "gbNVList";
            this.gbNVList.Padding = new System.Windows.Forms.Padding(2);
            this.gbNVList.Size = new System.Drawing.Size(978, 269);
            this.gbNVList.TabIndex = 2;
            this.gbNVList.TabStop = false;
            this.gbNVList.Text = "Danh sách NV";
            // 
            // dgvNhanVien
            // 
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhanVien.ColumnHeadersHeight = 40;
            this.dgvNhanVien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MSNV,
            this.HoTenNV,
            this.CoSo,
            this.Role,
            this.Notes_NV,
            this.DB_CoSo});
            this.dgvNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhanVien.Location = new System.Drawing.Point(2, 21);
            this.dgvNhanVien.Margin = new System.Windows.Forms.Padding(2);
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.RowHeadersVisible = false;
            this.dgvNhanVien.RowHeadersWidth = 72;
            this.dgvNhanVien.RowTemplate.Height = 31;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.Size = new System.Drawing.Size(974, 246);
            this.dgvNhanVien.TabIndex = 0;
            // 
            // MSNV
            // 
            this.MSNV.HeaderText = "MSNV";
            this.MSNV.MinimumWidth = 9;
            this.MSNV.Name = "MSNV";
            this.MSNV.ReadOnly = true;
            // 
            // HoTenNV
            // 
            this.HoTenNV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.HoTenNV.HeaderText = "Họ và Tên";
            this.HoTenNV.MinimumWidth = 9;
            this.HoTenNV.Name = "HoTenNV";
            this.HoTenNV.ReadOnly = true;
            this.HoTenNV.Width = 130;
            // 
            // CoSo
            // 
            this.CoSo.HeaderText = "Cơ sở";
            this.CoSo.MinimumWidth = 9;
            this.CoSo.Name = "CoSo";
            this.CoSo.ReadOnly = true;
            // 
            // Role
            // 
            this.Role.HeaderText = "Vai trò";
            this.Role.MinimumWidth = 9;
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
            // 
            // Notes_NV
            // 
            this.Notes_NV.HeaderText = "Ghi Chú";
            this.Notes_NV.MinimumWidth = 9;
            this.Notes_NV.Name = "Notes_NV";
            this.Notes_NV.ReadOnly = true;
            // 
            // DB_CoSo
            // 
            this.DB_CoSo.HeaderText = "Mã Cơ Sở";
            this.DB_CoSo.MinimumWidth = 9;
            this.DB_CoSo.Name = "DB_CoSo";
            this.DB_CoSo.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.lblQL_TotalNumber);
            this.panel1.Controls.Add(this.label35);
            this.panel1.Controls.Add(this.lblNV_TotalNumber);
            this.panel1.Controls.Add(this.label34);
            this.panel1.Controls.Add(this.btnTTExport);
            this.panel1.Controls.Add(this.btnDTExport);
            this.panel1.Controls.Add(this.btnQLExport);
            this.panel1.Controls.Add(this.btnNVExport);
            this.panel1.Controls.Add(this.gbTaiChinh);
            this.panel1.Controls.Add(this.gbTTList);
            this.panel1.Controls.Add(this.gbGeneralInfo);
            this.panel1.Controls.Add(this.gbDT_List);
            this.panel1.Controls.Add(this.gbQLList);
            this.panel1.Controls.Add(this.gbNVList);
            this.panel1.Location = new System.Drawing.Point(10, 118);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1032, 565);
            this.panel1.TabIndex = 3;
            // 
            // lblQL_TotalNumber
            // 
            this.lblQL_TotalNumber.AutoSize = true;
            this.lblQL_TotalNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQL_TotalNumber.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblQL_TotalNumber.Location = new System.Drawing.Point(838, 574);
            this.lblQL_TotalNumber.Name = "lblQL_TotalNumber";
            this.lblQL_TotalNumber.Size = new System.Drawing.Size(0, 22);
            this.lblQL_TotalNumber.TabIndex = 14;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(674, 574);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(154, 22);
            this.label35.TabIndex = 13;
            this.label35.Text = "Tổng số Quản Lý:";
            // 
            // lblNV_TotalNumber
            // 
            this.lblNV_TotalNumber.AutoSize = true;
            this.lblNV_TotalNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNV_TotalNumber.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblNV_TotalNumber.Location = new System.Drawing.Point(826, 240);
            this.lblNV_TotalNumber.Name = "lblNV_TotalNumber";
            this.lblNV_TotalNumber.Size = new System.Drawing.Size(0, 22);
            this.lblNV_TotalNumber.TabIndex = 12;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(674, 240);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(167, 22);
            this.label34.TabIndex = 11;
            this.label34.Text = "Tổng số Nhân viên:";
            // 
            // btnTTExport
            // 
            this.btnTTExport.BackColor = System.Drawing.Color.Transparent;
            this.btnTTExport.Location = new System.Drawing.Point(44, 1286);
            this.btnTTExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnTTExport.Name = "btnTTExport";
            this.btnTTExport.Size = new System.Drawing.Size(110, 31);
            this.btnTTExport.TabIndex = 6;
            this.btnTTExport.Text = "Xuất Excel";
            this.btnTTExport.UseVisualStyleBackColor = false;
            this.btnTTExport.Click += new System.EventHandler(this.btnTTExport_Click);
            // 
            // btnDTExport
            // 
            this.btnDTExport.BackColor = System.Drawing.Color.Transparent;
            this.btnDTExport.Location = new System.Drawing.Point(44, 926);
            this.btnDTExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnDTExport.Name = "btnDTExport";
            this.btnDTExport.Size = new System.Drawing.Size(110, 31);
            this.btnDTExport.TabIndex = 5;
            this.btnDTExport.Text = "Xuất Excel";
            this.btnDTExport.UseVisualStyleBackColor = false;
            this.btnDTExport.Click += new System.EventHandler(this.btnDTExport_Click);
            // 
            // btnQLExport
            // 
            this.btnQLExport.BackColor = System.Drawing.Color.Transparent;
            this.btnQLExport.Location = new System.Drawing.Point(44, 555);
            this.btnQLExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnQLExport.Name = "btnQLExport";
            this.btnQLExport.Size = new System.Drawing.Size(110, 31);
            this.btnQLExport.TabIndex = 4;
            this.btnQLExport.Text = "Xuất Excel";
            this.btnQLExport.UseVisualStyleBackColor = false;
            this.btnQLExport.Click += new System.EventHandler(this.btnQLExport_Click);
            // 
            // btnNVExport
            // 
            this.btnNVExport.BackColor = System.Drawing.Color.Transparent;
            this.btnNVExport.Location = new System.Drawing.Point(44, 231);
            this.btnNVExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnNVExport.Name = "btnNVExport";
            this.btnNVExport.Size = new System.Drawing.Size(110, 31);
            this.btnNVExport.TabIndex = 3;
            this.btnNVExport.Text = "Xuất Excel";
            this.btnNVExport.UseVisualStyleBackColor = false;
            this.btnNVExport.Click += new System.EventHandler(this.btnNVExport_Click);
            // 
            // gbTaiChinh
            // 
            this.gbTaiChinh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTaiChinh.Controls.Add(this.numUEF);
            this.gbTaiChinh.Controls.Add(this.numTaiTro);
            this.gbTaiChinh.Controls.Add(this.txtTC_Khac);
            this.gbTaiChinh.Controls.Add(this.txtTC_TieuDe);
            this.gbTaiChinh.Controls.Add(this.label29);
            this.gbTaiChinh.Controls.Add(this.label28);
            this.gbTaiChinh.Controls.Add(this.label30);
            this.gbTaiChinh.Controls.Add(this.label32);
            this.gbTaiChinh.Controls.Add(this.label31);
            this.gbTaiChinh.Controls.Add(this.label27);
            this.gbTaiChinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTaiChinh.Location = new System.Drawing.Point(16, 1658);
            this.gbTaiChinh.Margin = new System.Windows.Forms.Padding(2);
            this.gbTaiChinh.Name = "gbTaiChinh";
            this.gbTaiChinh.Padding = new System.Windows.Forms.Padding(2);
            this.gbTaiChinh.Size = new System.Drawing.Size(976, 291);
            this.gbTaiChinh.TabIndex = 2;
            this.gbTaiChinh.TabStop = false;
            this.gbTaiChinh.Text = "Thông tin Tài Chính";
            // 
            // numUEF
            // 
            this.numUEF.AlwaysActive = false;
            this.numUEF.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numUEF.Location = new System.Drawing.Point(232, 75);
            this.numUEF.Margin = new System.Windows.Forms.Padding(2);
            this.numUEF.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numUEF.Name = "numUEF";
            this.numUEF.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.numUEF.ReadOnly = true;
            this.numUEF.Size = new System.Drawing.Size(243, 30);
            this.numUEF.TabIndex = 3;
            this.numUEF.ThousandsSeparator = true;
            this.numUEF.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numUEF.UpDownButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Form;
            // 
            // numTaiTro
            // 
            this.numTaiTro.AlwaysActive = false;
            this.numTaiTro.Increment = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numTaiTro.Location = new System.Drawing.Point(232, 115);
            this.numTaiTro.Margin = new System.Windows.Forms.Padding(2);
            this.numTaiTro.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.numTaiTro.Name = "numTaiTro";
            this.numTaiTro.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Blue;
            this.numTaiTro.ReadOnly = true;
            this.numTaiTro.Size = new System.Drawing.Size(243, 30);
            this.numTaiTro.TabIndex = 3;
            this.numTaiTro.ThousandsSeparator = true;
            this.numTaiTro.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numTaiTro.UpDownButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Form;
            // 
            // txtTC_Khac
            // 
            this.txtTC_Khac.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTC_Khac.Location = new System.Drawing.Point(232, 155);
            this.txtTC_Khac.Margin = new System.Windows.Forms.Padding(2);
            this.txtTC_Khac.Multiline = true;
            this.txtTC_Khac.Name = "txtTC_Khac";
            this.txtTC_Khac.ReadOnly = true;
            this.txtTC_Khac.Size = new System.Drawing.Size(504, 98);
            this.txtTC_Khac.TabIndex = 2;
            // 
            // txtTC_TieuDe
            // 
            this.txtTC_TieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTC_TieuDe.Location = new System.Drawing.Point(232, 42);
            this.txtTC_TieuDe.Margin = new System.Windows.Forms.Padding(2);
            this.txtTC_TieuDe.Name = "txtTC_TieuDe";
            this.txtTC_TieuDe.ReadOnly = true;
            this.txtTC_TieuDe.Size = new System.Drawing.Size(538, 28);
            this.txtTC_TieuDe.TabIndex = 2;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(22, 45);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(75, 22);
            this.label29.TabIndex = 0;
            this.label29.Text = "Tiêu đề:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(22, 80);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(145, 22);
            this.label28.TabIndex = 0;
            this.label28.Text = "Tổng tiền đã chi:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(22, 155);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(48, 22);
            this.label30.TabIndex = 0;
            this.label30.Text = "Khác";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(476, 80);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(45, 22);
            this.label32.TabIndex = 0;
            this.label32.Text = "VND";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(476, 118);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(45, 22);
            this.label31.TabIndex = 0;
            this.label31.Text = "VND";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(22, 118);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(145, 22);
            this.label27.TabIndex = 0;
            this.label27.Text = "Tổng tiền tài trợ:";
            // 
            // gbTTList
            // 
            this.gbTTList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTTList.Controls.Add(this.dgvTaiTro);
            this.gbTTList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTTList.Location = new System.Drawing.Point(16, 1328);
            this.gbTTList.Margin = new System.Windows.Forms.Padding(2);
            this.gbTTList.Name = "gbTTList";
            this.gbTTList.Padding = new System.Windows.Forms.Padding(2);
            this.gbTTList.Size = new System.Drawing.Size(978, 302);
            this.gbTTList.TabIndex = 2;
            this.gbTTList.TabStop = false;
            this.gbTTList.Text = "Danh sách Tài Trợ";
            // 
            // dgvTaiTro
            // 
            this.dgvTaiTro.AllowUserToAddRows = false;
            this.dgvTaiTro.AllowUserToDeleteRows = false;
            this.dgvTaiTro.AllowUserToOrderColumns = true;
            this.dgvTaiTro.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTaiTro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaiTro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TT_Name,
            this.TT_Rep,
            this.TT_SDT,
            this.TT_Email,
            this.TT_Notes,
            this.TT_IDDB});
            this.dgvTaiTro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaiTro.Location = new System.Drawing.Point(2, 21);
            this.dgvTaiTro.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTaiTro.Name = "dgvTaiTro";
            this.dgvTaiTro.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.dgvTaiTro.ReadOnly = true;
            this.dgvTaiTro.RowHeadersVisible = false;
            this.dgvTaiTro.RowHeadersWidth = 72;
            this.dgvTaiTro.RowTemplate.Height = 31;
            this.dgvTaiTro.Size = new System.Drawing.Size(974, 279);
            this.dgvTaiTro.TabIndex = 0;
            // 
            // TT_Name
            // 
            this.TT_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TT_Name.Frozen = true;
            this.TT_Name.HeaderText = "Tên Nhà Tài Trợ";
            this.TT_Name.MinimumWidth = 9;
            this.TT_Name.Name = "TT_Name";
            this.TT_Name.ReadOnly = true;
            this.TT_Name.Width = 169;
            // 
            // TT_Rep
            // 
            this.TT_Rep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TT_Rep.HeaderText = "Đại Diện";
            this.TT_Rep.MinimumWidth = 9;
            this.TT_Rep.Name = "TT_Rep";
            this.TT_Rep.ReadOnly = true;
            this.TT_Rep.Width = 111;
            // 
            // TT_SDT
            // 
            this.TT_SDT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TT_SDT.HeaderText = "Số Điện Thoại";
            this.TT_SDT.MinimumWidth = 9;
            this.TT_SDT.Name = "TT_SDT";
            this.TT_SDT.ReadOnly = true;
            this.TT_SDT.Width = 149;
            // 
            // TT_Email
            // 
            this.TT_Email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TT_Email.HeaderText = "Email";
            this.TT_Email.MinimumWidth = 9;
            this.TT_Email.Name = "TT_Email";
            this.TT_Email.ReadOnly = true;
            this.TT_Email.Width = 94;
            // 
            // TT_Notes
            // 
            this.TT_Notes.HeaderText = "Nội Dung";
            this.TT_Notes.MinimumWidth = 9;
            this.TT_Notes.Name = "TT_Notes";
            this.TT_Notes.ReadOnly = true;
            // 
            // TT_IDDB
            // 
            this.TT_IDDB.HeaderText = "ID_DB";
            this.TT_IDDB.MinimumWidth = 9;
            this.TT_IDDB.Name = "TT_IDDB";
            this.TT_IDDB.ReadOnly = true;
            // 
            // gbDT_List
            // 
            this.gbDT_List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDT_List.Controls.Add(this.dgvDoiTac);
            this.gbDT_List.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDT_List.Location = new System.Drawing.Point(16, 966);
            this.gbDT_List.Margin = new System.Windows.Forms.Padding(2);
            this.gbDT_List.Name = "gbDT_List";
            this.gbDT_List.Padding = new System.Windows.Forms.Padding(2);
            this.gbDT_List.Size = new System.Drawing.Size(978, 302);
            this.gbDT_List.TabIndex = 2;
            this.gbDT_List.TabStop = false;
            this.gbDT_List.Text = "Danh sách Đối Tác";
            // 
            // dgvDoiTac
            // 
            this.dgvDoiTac.AllowUserToAddRows = false;
            this.dgvDoiTac.AllowUserToDeleteRows = false;
            this.dgvDoiTac.AllowUserToOrderColumns = true;
            this.dgvDoiTac.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoiTac.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoiTac.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DT_Ten,
            this.DT_DaiDien,
            this.DT_SDT,
            this.DT_Email,
            this.DT_NoiDung,
            this.ID_DB});
            this.dgvDoiTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoiTac.Location = new System.Drawing.Point(2, 21);
            this.dgvDoiTac.Margin = new System.Windows.Forms.Padding(2);
            this.dgvDoiTac.Name = "dgvDoiTac";
            this.dgvDoiTac.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.dgvDoiTac.ReadOnly = true;
            this.dgvDoiTac.RowHeadersVisible = false;
            this.dgvDoiTac.RowHeadersWidth = 72;
            this.dgvDoiTac.RowTemplate.Height = 31;
            this.dgvDoiTac.Size = new System.Drawing.Size(974, 279);
            this.dgvDoiTac.TabIndex = 0;
            // 
            // DT_Ten
            // 
            this.DT_Ten.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DT_Ten.Frozen = true;
            this.DT_Ten.HeaderText = "Tên Đối Tác";
            this.DT_Ten.MinimumWidth = 9;
            this.DT_Ten.Name = "DT_Ten";
            this.DT_Ten.ReadOnly = true;
            this.DT_Ten.Width = 140;
            // 
            // DT_DaiDien
            // 
            this.DT_DaiDien.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DT_DaiDien.HeaderText = "Đại Diện";
            this.DT_DaiDien.MinimumWidth = 9;
            this.DT_DaiDien.Name = "DT_DaiDien";
            this.DT_DaiDien.ReadOnly = true;
            this.DT_DaiDien.Width = 111;
            // 
            // DT_SDT
            // 
            this.DT_SDT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DT_SDT.HeaderText = "Số Điện Thoại";
            this.DT_SDT.MinimumWidth = 9;
            this.DT_SDT.Name = "DT_SDT";
            this.DT_SDT.ReadOnly = true;
            this.DT_SDT.Width = 149;
            // 
            // DT_Email
            // 
            this.DT_Email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DT_Email.HeaderText = "Email";
            this.DT_Email.MinimumWidth = 9;
            this.DT_Email.Name = "DT_Email";
            this.DT_Email.ReadOnly = true;
            this.DT_Email.Width = 94;
            // 
            // DT_NoiDung
            // 
            this.DT_NoiDung.HeaderText = "Nội Dung";
            this.DT_NoiDung.MinimumWidth = 9;
            this.DT_NoiDung.Name = "DT_NoiDung";
            this.DT_NoiDung.ReadOnly = true;
            // 
            // ID_DB
            // 
            this.ID_DB.HeaderText = "ID_DB";
            this.ID_DB.MinimumWidth = 9;
            this.ID_DB.Name = "ID_DB";
            this.ID_DB.ReadOnly = true;
            // 
            // gbQLList
            // 
            this.gbQLList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbQLList.Controls.Add(this.dgv_QL);
            this.gbQLList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbQLList.Location = new System.Drawing.Point(16, 598);
            this.gbQLList.Margin = new System.Windows.Forms.Padding(2);
            this.gbQLList.Name = "gbQLList";
            this.gbQLList.Padding = new System.Windows.Forms.Padding(2);
            this.gbQLList.Size = new System.Drawing.Size(978, 302);
            this.gbQLList.TabIndex = 2;
            this.gbQLList.TabStop = false;
            this.gbQLList.Text = "Danh sách QL";
            // 
            // dgv_QL
            // 
            this.dgv_QL.AllowUserToAddRows = false;
            this.dgv_QL.AllowUserToDeleteRows = false;
            this.dgv_QL.AllowUserToOrderColumns = true;
            this.dgv_QL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_QL.ColumnHeadersHeight = 40;
            this.dgv_QL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaQL,
            this.HoTenLot,
            this.Ten,
            this.QLCoSo,
            this.QL_Role,
            this.QLCS_DB});
            this.dgv_QL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_QL.Location = new System.Drawing.Point(2, 21);
            this.dgv_QL.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_QL.Name = "dgv_QL";
            this.dgv_QL.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2010Silver;
            this.dgv_QL.ReadOnly = true;
            this.dgv_QL.RowHeadersVisible = false;
            this.dgv_QL.RowHeadersWidth = 72;
            this.dgv_QL.RowTemplate.Height = 31;
            this.dgv_QL.Size = new System.Drawing.Size(974, 279);
            this.dgv_QL.TabIndex = 0;
            // 
            // MaQL
            // 
            this.MaQL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.MaQL.Frozen = true;
            this.MaQL.HeaderText = "Mã Giảng Viên";
            this.MaQL.MinimumWidth = 9;
            this.MaQL.Name = "MaQL";
            this.MaQL.ReadOnly = true;
            this.MaQL.Width = 167;
            // 
            // HoTenLot
            // 
            this.HoTenLot.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.HoTenLot.HeaderText = "Họ và Tên Lót";
            this.HoTenLot.MinimumWidth = 9;
            this.HoTenLot.Name = "HoTenLot";
            this.HoTenLot.ReadOnly = true;
            this.HoTenLot.Width = 160;
            // 
            // Ten
            // 
            this.Ten.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Ten.HeaderText = "Tên";
            this.Ten.MinimumWidth = 9;
            this.Ten.Name = "Ten";
            this.Ten.ReadOnly = true;
            this.Ten.Width = 78;
            // 
            // QLCoSo
            // 
            this.QLCoSo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.QLCoSo.HeaderText = "Cơ sở";
            this.QLCoSo.MinimumWidth = 9;
            this.QLCoSo.Name = "QLCoSo";
            this.QLCoSo.ReadOnly = true;
            this.QLCoSo.Width = 98;
            // 
            // QL_Role
            // 
            this.QL_Role.HeaderText = "Vai trò";
            this.QL_Role.MinimumWidth = 9;
            this.QL_Role.Name = "QL_Role";
            this.QL_Role.ReadOnly = true;
            // 
            // QLCS_DB
            // 
            this.QLCS_DB.HeaderText = "Mã Cơ Sở";
            this.QLCS_DB.MinimumWidth = 9;
            this.QLCS_DB.Name = "QLCS_DB";
            this.QLCS_DB.ReadOnly = true;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(903, 11);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(118, 45);
            this.btnExit.TabIndex = 4;
            this.btnExit.Values.Text = "Thoát";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 703);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1052, 66);
            this.panel2.TabIndex = 5;
            // 
            // frmViewHoatDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(1052, 769);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblHeader);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmViewHoatDong";
            this.Text = "Thông Tin Hoạt Động";
            this.Load += new System.EventHandler(this.frmViewHoatDong_Load);
            this.gbGeneralInfo.ResumeLayout(false);
            this.gbGeneralInfo.PerformLayout();
            this.gbNVList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbTaiChinh.ResumeLayout(false);
            this.gbTaiChinh.PerformLayout();
            this.gbTTList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiTro)).EndInit();
            this.gbDT_List.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoiTac)).EndInit();
            this.gbQLList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_QL)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox gbGeneralInfo;
        private System.Windows.Forms.TextBox txtTenHD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbNVList;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvNhanVien;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnExit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbQLList;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgv_QL;
        private System.Windows.Forms.GroupBox gbDT_List;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvDoiTac;
        private System.Windows.Forms.GroupBox gbTTList;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvTaiTro;
        private System.Windows.Forms.GroupBox gbTaiChinh;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtTC_TieuDe;
        private System.Windows.Forms.TextBox txtTC_Khac;
        private System.Windows.Forms.Label label30;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown numTaiTro;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown numUEF;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtDateEnd;
        private System.Windows.Forms.TextBox txtDateBegin;
        private System.Windows.Forms.TextBox txtLoai;
        private System.Windows.Forms.Button btnNVExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT_Rep;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT_SDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT_Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT_Notes;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT_IDDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_Ten;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_DaiDien;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_SDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_NoiDung;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_DB;
        private System.Windows.Forms.Button btnQLExport;
        private System.Windows.Forms.Button btnTTExport;
        private System.Windows.Forms.Button btnDTExport;
        private System.Windows.Forms.Label lblNV_TotalNumber;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label lblQL_TotalNumber;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.DataGridViewTextBoxColumn MSNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoTenNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes_NV;
        private System.Windows.Forms.DataGridViewTextBoxColumn DB_CoSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaQL;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoTenLot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ten;
        private System.Windows.Forms.DataGridViewTextBoxColumn QLCoSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn QL_Role;
        private System.Windows.Forms.DataGridViewTextBoxColumn QLCS_DB;
    }
}