using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DoAnMNM.Models;

namespace DoAnMNM
{
    public partial class FormQLKTX : Form
    {

        private string maQL;
        ModelQLKTX db = new ModelQLKTX();

        public FormQLKTX()
        {
            InitializeComponent();
            HideTab();
        }

        public FormQLKTX(String maQL)
        {

            InitializeComponent();
            HideTab();
            this.maQL = maQL;
            lbQuanLy.Text = db.QuanLies.ToList().Find(a => a.MaQL == this.maQL).HoTenQL;           
        }

        private void HideTab()
        {
            tabControlKTX.Appearance = TabAppearance.FlatButtons;
            tabControlKTX.ItemSize = new Size(0, 1);
            tabControlKTX.SizeMode = TabSizeMode.Fixed;
            tabControlKTX.DrawMode = TabDrawMode.OwnerDrawFixed;
            foreach (TabPage tab in tabControlKTX.TabPages)
            {
                tab.Text = "";
            }
        }

        private void KhoiTao_DDK()
        {
            cboThoiGian_DKM.Items.Clear();
            cboPhong_DKM.Items.Clear();
            txtMSSV_DKM.Text = "";
            txtHoTen_DKM.Text = "";
            txtCMND_DKM.Text = "";
            txtSDT_DKM.Text = "";
            txtEmail_DKM.Text = "";
            rbNam_DKM.Checked = true;
            dtpNgaySinh_DKM.Value = DateTime.Now;
            dtpNgayVao_DKM.Value = DateTime.Now;
            for (int i = 1; i <= 4; i++)
            {
                cboThoiGian_DKM.Items.Add(i);
            }
            cboThoiGian_DKM.SelectedIndex = 0;
            List<Phong> dsPhong = db.Phongs.ToList();
            foreach (var x in dsPhong)
            {
                cboPhong_DKM.Items.Add(x);
            }
            cboPhong_DKM.SelectedIndex = 0;
        }

        private void KhoiTao_DSDDK()
        {
            List<DonDangKy> dsDDK = db.DonDangKies.ToList();
            txtMaQL_DSDDK.Text = "";           
            HienThi_DSDDK(dsDDK);
        }

        private void HienThi_DSDDK(List<DonDangKy> dsDDK)
        {
            dataDSDDK.Rows.Clear();
            foreach (var x in dsDDK)
            {
                dataDSDDK.Rows.Add(new object[] {
                    x.MaDonDangKy,
                    x.MaQL,
                    x.MSSV,
                    x.NgayVao.ToShortDateString(),
                    x.ThoiGian,
                    x.NgayLamDon.ToShortDateString()});
            }
        }

        private void btnDangXuat_QL_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSua_QL_Click(object sender, EventArgs e)
        {
            QuanLy ql = db.QuanLies.Find(maQL);
            txtMaQL_SuaQL.Text = ql.MaQL.ToString();
            txtHoTen_SuaQL.Text = ql.HoTenQL.ToString();
            if (ql.GioiTinh == true)
                rdbNam_SuaQL.Checked = true;
            else
                rdbNu_SuaQL.Checked = true;
            dtpNgaySinh_SuaQL.Value = DateTime.Parse(ql.NgaySinh.ToString());
            txtSDT_SuaQL.Text = ql.SDT.ToString();
            txtCMND_SuaQL.Text = ql.CMND.ToString();
            txtEMAIL_SuaQL.Text = ql.DiaChi.ToString();
            txtMatKhau_SuaQL.Text = ql.MatKhau.ToString();

            tabControlKTX.SelectedTab = tpSuaQL;
        }

        private void btnSua_SuaQL_Click(object sender, EventArgs e)
        {
            QuanLy ql = db.QuanLies.Find(txtMaQL_SuaQL.Text.ToString());
            ql.HoTenQL = txtHoTen_SuaQL.Text;
            if (rdbNam_SuaQL.Checked)
                ql.GioiTinh = true;
            else
                ql.GioiTinh = false;
            ql.NgaySinh = dtpNgaySinh_SuaQL.Value;
            ql.SDT = txtSDT_SuaQL.Text;
            ql.CMND = txtCMND_SuaQL.Text;
            ql.DiaChi = txtEMAIL_SuaQL.Text;
            ql.MatKhau = txtMatKhau_SuaQL.Text;
            db.SaveChanges();

            MessageBox.Show("Đã cập nhật thông tin quản lý");
        }

        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpThemPhong;
            KhoiTao_ThemPhong();

        }

        private void KhoiTao_ThemPhong()
        {
            cboLoaiPhong_ThemPhong.Items.Clear();
            cboKhu_ThemPhong.Items.Clear();
            txtMaPhong_ThemPhong.Text = "";
            txtTenPhong_ThemPhong.Text = "";
            List<LoaiPhong> dsLoaiPhong = db.LoaiPhongs.ToList();
            foreach (LoaiPhong x in dsLoaiPhong)
            {
                cboLoaiPhong_ThemPhong.Items.Add(x);
            }
            cboLoaiPhong_ThemPhong.SelectedIndex = 0;
            List<Khu> dsKhu = db.Khus.ToList();
            foreach (Khu x in dsKhu)
            {
                cboKhu_ThemPhong.Items.Add(x);
            }
            cboKhu_ThemPhong.SelectedIndex = 0;
        }

        private void btnThem_ThemPhong_Click(object sender, EventArgs e)
        {
            if (txtTenPhong_ThemPhong.Text == "" || txtMaPhong_ThemPhong.Text == "")
            {
                DialogResult result = MessageBox.Show("Không được để trống", "Không thêm được", MessageBoxButtons.OK);
            }
            else
            {
                Phong p = new Phong();
                p.MaPhong = txtMaPhong_ThemPhong.Text;
                p.TenPhong = txtTenPhong_ThemPhong.Text;
                p.MaKhu = ((Khu)cboKhu_ThemPhong.SelectedItem).MaKhu;
                p.MaLoaiPhong = ((LoaiPhong)cboLoaiPhong_ThemPhong.SelectedItem).MaLoaiPhong;
                p.MaQL = maQL;
                p.SoDien = 0;
                db.Phongs.Add(p);
                db.SaveChanges();
                KhoiTao_ThemPhong();
            }
        }

        private void btnDSPhong_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpDanhSachPhong;
            KhoiTao_DSPhong();
        }

        private void KhoiTao_DSPhong()
        {
            cboLoaiPhong_DSPhong.Items.Clear();
            cboKhu_DSPhong.Items.Clear();
            txtMaPhong_DSPhong.Text = "";
            List<Phong> dsPhong = db.Phongs.ToList();
            List<LoaiPhong> dsLoaiPhong = db.LoaiPhongs.ToList();
            foreach (var x in dsLoaiPhong)
            {
                cboLoaiPhong_DSPhong.Items.Add(x);
            }

            List<Khu> dsKhu = db.Khus.ToList();
            foreach (var x in dsKhu)
            {
                cboKhu_DSPhong.Items.Add(x);
            }
            HienThiPhong_DSPhong(dsPhong);
        }

        private void HienThiPhong_DSPhong(List<Phong> dsPhong)
        {
            tableLayoutPanelDSPhong.Controls.Clear();

            foreach (var x in dsPhong)
            {
                //Tạo picture
                PictureBox pic = new PictureBox();
                pic.Name = x.MaPhong;
                pic.BackColor = System.Drawing.Color.Transparent;
                pic.Dock = System.Windows.Forms.DockStyle.Fill;
                pic.Image = global::DoAnMNM.Properties.Resources.home_2;
                //pic.Image=global::DACNQuanLyKTX.Properties.Resources.
                pic.Location = new System.Drawing.Point(0, 0);
                //pic.Size = new System.Drawing.Size(119, 105);
                pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
                pic.TabIndex = 0;
                pic.TabStop = false;
                pic.MouseHover += Pic_MouseHover;
                pic.MouseLeave += Pic_MouseLeave;
                pic.MouseClick += Pic_MouseClick;


                //Tạo label
                Label l = new Label();
                l.AutoSize = true;
                l.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                l.ForeColor = System.Drawing.Color.Gainsboro;
                l.Location = new System.Drawing.Point(24, 114);
                l.Size = new System.Drawing.Size(50, 17);
                l.TabIndex = 1;
                l.Text = x.MaPhong + ": " + timSoSinhVienTrongPhong(x) + "/" + x.LoaiPhong.TongSoGiuong;
                l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                //tạo panel
                Panel p = new Panel();
                p.Controls.Add(l);
                p.Controls.Add(pic);
                //p.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(56)))), ((int)(((byte)(85)))));
                p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                p.Dock = System.Windows.Forms.DockStyle.Fill;
                p.Location = new System.Drawing.Point(3, 3);
                p.Size = new System.Drawing.Size(119, 144);
                p.TabIndex = 0;
                //thêm panel vào layout
                int a = tableLayoutPanelDSPhong.Controls.Count;

                tableLayoutPanelDSPhong.Controls.Add(p);
            }
        }

        private void Pic_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pic = ((PictureBox)sender);
            //int index = dsPic.FindIndex(a => a == (PictureBox)sender);
            Phong p = db.Phongs.Find(pic.Name);
            tabControlKTX.SelectedTab = tpSuaPhong;
            cboLoaiPhong_ThemPhong.Items.Clear();
            cboKhu_ThemPhong.Items.Clear();
            txtMaPhong_SuaPhong.Text = p.MaPhong;
            txtTenPhong_SuaPhong.Text = p.TenPhong;
            List<LoaiPhong> dsLoaiPhong = db.LoaiPhongs.ToList();
            foreach (LoaiPhong x in dsLoaiPhong)
            {
                cboLoaiPhong_SuaPhong.Items.Add(x);
            }
            cboLoaiPhong_SuaPhong.SelectedItem = dsLoaiPhong.Find(a => a.MaLoaiPhong == p.MaLoaiPhong);
            List<Khu> dsKhu = db.Khus.ToList();
            foreach (Khu x in dsKhu)
            {
                cboKhu_SuaPhong.Items.Add(x);
            }
            cboKhu_SuaPhong.SelectedItem = dsKhu.Find(a => a.MaKhu == p.MaKhu);
        }

        private void Pic_MouseHover(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(56)))), ((int)(((byte)(85)))));
        }

        private void Pic_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = System.Drawing.Color.Transparent;
        }

        private int timSoSinhVienTrongPhong(Phong p)
        {
            int sum = 0;
            foreach (var x in p.SinhViens)
            {
                //Nếu sinh viên có ít nhất một đơn đăn ký có thời gian hợp lệ (Đang có hiệu lực ở)
                //Tính ngày hết hạn đơn đăng ký bằng ngày vào cộng với thời gian ở
                if (x.DonDangKies.Where(a => (a.NgayVao.AddYears(a.ThoiGian)) >= DateTime.Now).Count() > 0)
                {
                    sum += 1;
                }
            }
            return sum;
        }

        private void xoaPhong() { }
		
        private void btnDonDangKy_Click(object sender, EventArgs e)
        {
            if (panelSubDonDangKy.Visible == false)
            {
                panelSubDonDangKy.Visible = true;
                btnDonDangKy.Image = DoAnMNM.Properties.Resources.icons8_collapse_arrow_16;
            }
            else
            {
                panelSubDonDangKy.Visible = false;
                btnDonDangKy.Image = DoAnMNM.Properties.Resources.icons8_expand_arrow_16;
            }
        }

        private void btnDDK_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpDangKyMoi;
            KhoiTao_DDK();
        }

        private void btnDSDDK_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpDSDDK;
            KhoiTao_DSDDK();
        }

        private void btnTimKiem_DSDDK_Click(object sender, EventArgs e)
        {

        }

        private void btnSua_DSD_Click(object sender, EventArgs e)
        {
            int Index = dataDSDDK.CurrentCell.RowIndex;

            string maddk = dataDSDDK.Rows[Index].Cells[0].Value.ToString();          
            DonDangKy ddk = db.DonDangKies.Find(maddk);
            txtMSSV_SuaDDK.Text = ddk.MSSV;
            txtMaDDK_SuaDDK.Text = ddk.MaDonDangKy;
            txtMaQL_SuaDDK.Text = ddk.MaQL;
            dtpNgayVao_SuaDDK.Value = DateTime.Parse(ddk.NgayVao.ToString());
            dtpNgapLap_SuaDDK.Value = DateTime.Parse(ddk.NgayLamDon.ToString());

            for (int i = 1; i <= 4; i++)
            {
                cboThoiGian_SuaDDK.Items.Add(i);
            }

            cboThoiGian_SuaDDK.SelectedIndex = ddk.ThoiGian - 1;
            tabControlKTX.SelectedTab = tpSuaDDK;
        }

        private void btnXoa_DSD_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Không thể xóa Đơn Đăng Ký", "Không xóa được", MessageBoxButtons.OK);
        }

        private void btnQuayLai_SuaDDK_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpDSDDK;
            KhoiTao_DSDDK();
        }

        private void btnSua_SuaDDK_Click(object sender, EventArgs e)
        {
            DonDangKy ddk = db.DonDangKies.Find(txtMaDDK_SuaDDK.Text);
            ddk.NgayVao = dtpNgayVao_SuaDDK.Value;
            ddk.ThoiGian = (int)cboThoiGian_SuaDDK.SelectedItem;
            ddk.NgayLamDon = DateTime.Now;
            db.SaveChanges();

            tabControlKTX.SelectedTab = tpDSDDK;
            KhoiTao_DSDDK();
        }

        private void btnTatCa_DSDDK_Click(object sender, EventArgs e)
        {

        }

        private void btnDangKy_DKM_Click(object sender, EventArgs e)
        {
            if (txtHoTen_DKM.Text == "" || txtMSSV_DKM.Text == "" || txtCMND_DKM.Text == "" || txtEmail_DKM.Text == "" || txtSDT_DKM.Text == "")
            {
                DialogResult result = MessageBox.Show("Không được để trống", "Không thêm được", MessageBoxButtons.OK);
            }
            else
            {
                //Thêm sinh viên
                List<SinhVien> dsSV = db.SinhViens.ToList();
                SinhVien sv = dsSV.Find(a => a.MSSV == txtMSSV_DKM.Text);
                if (!(dsSV.Contains(sv)))
                {
                    sv = new SinhVien();
                    sv.MSSV = txtMSSV_DKM.Text;
                    sv.HoTenSV = txtHoTen_DKM.Text;
                    if (rbNam_DKM.Checked)
                        sv.GioiTinh = true;
                    else
                        sv.GioiTinh = false;
                    sv.NgaySinh = dtpNgaySinh_DKM.Value;
                    sv.SDT = txtSDT_DKM.Text;
                    sv.CMND = txtCMND_DKM.Text;
                    sv.DiaChi = txtEmail_DKM.Text;
                    sv.MaPhong = ((Phong)cboPhong_DKM.SelectedItem).MaPhong;
                    db.SinhViens.Add(sv);
                }
                //Thêm đơn đăng ký mới
                DonDangKy donDK = new DonDangKy();
                donDK.MSSV = txtMSSV_DKM.Text;
                donDK.MaDonDangKy = "DDK" + db.DonDangKies.Count();
                donDK.MaQL = maQL;
                donDK.MSSV = sv.MSSV;
                donDK.NgayVao = dtpNgayVao_DKM.Value;
                donDK.ThoiGian = (int)cboThoiGian_DKM.SelectedItem;
                donDK.NgayLamDon = DateTime.Now;
                db.DonDangKies.Add(donDK);
                db.SaveChanges();
                KhoiTao_DDK();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpQLDonGia;
            KhoiTao_QLDonGia();
        }

        private void KhoiTao_QLDonGia()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = string.Format("{0}Resources\\DonGiaPhong.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
            string FileName1 = string.Format("{0}Resources\\DonGiaDien.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
			string donGiaPhong;
            using (StreamReader sr = new StreamReader(FileName))
            {
                donGiaPhong = sr.ReadToEnd();
            }
            string donGiaDien;
            using (StreamReader sr1 = new StreamReader(FileName1))
            {
                donGiaDien = sr1.ReadToEnd();
            }
            txtDGD_QLDonGia.Text = donGiaDien;
            txtDGP_QLDonGia.Text= donGiaPhong;
        }
		
        private void btnHDP_Click(object sender, EventArgs e)
        {
            if (panelSubHDP.Visible == false)
            {
                panelSubHDP.Visible = true;
                btnHDP.Image = DoAnMNM.Properties.Resources.icons8_collapse_arrow_16;
            }
            else
            {
                panelSubHDP.Visible = false;
                btnHDP.Image = DoAnMNM.Properties.Resources.icons8_expand_arrow_16;
            }
        }

        private void btnThemHDP_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpThemHDP;
            KhoiTao_HDP();
        }

        private void btnDSHDP_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpDSHDP;
            KhoiTao_DSHDP();
        }

        private void KhoiTao_HDP()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = string.Format("{0}Resources\\DonGiaPhong.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
            string donGiaPhong;
            using (StreamReader sr = new StreamReader(FileName))
            {
                donGiaPhong = sr.ReadToEnd();
            }
            string donGiaDien;
            using (StreamReader sr1 = new StreamReader(FileName1))
            {
                donGiaDien = sr1.ReadToEnd();
            }
            txtDGD_QLDonGia.Text = donGiaDien;
            txtDGP_QLDonGia.Text = donGiaPhong;
        }

        private void btnSuaDGD_QLDonGia_Click(object sender, EventArgs e)
        {
            string donGiaDien = txtDGD_QLDonGia.Text;
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName1 = string.Format("{0}Resources\\DonGiaDien.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));

            using (StreamWriter sw = new StreamWriter(FileName1))
            {
                sw.Write(donGiaDien);
            }
            MessageBox.Show("Bạn đã cập nhật đơn giá điện thành công!");
        }

        private void btnSuaDGP_QLDonGia_Click(object sender, EventArgs e)
        {
            string donGiaPhong = txtDGP_QLDonGia.Text;
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = string.Format("{0}Resources\\DonGiaPhong.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));

            using (StreamWriter sw = new StreamWriter(FileName))
            {
                sw.Write(donGiaPhong);
            }
            MessageBox.Show("Bạn đã cập nhật đơn giá phòng thành công!");
		}

        private void btnHDD_Click(object sender, EventArgs e)
        {
            if (panelSubHDD.Visible == false)
            {
                panelSubHDD.Visible = true;
                btnHDD.Image = DoAnMNM.Properties.Resources.icons8_collapse_arrow_16;
            }
            else
            {
                panelSubHDD.Visible = false;
                btnHDD.Image = DoAnMNM.Properties.Resources.icons8_expand_arrow_16;
            }
        }

        private void btnThemHDD_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpThemHDD;
            khoiTao_HDD();
        }

        private void btnDSHDP_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpDSHDD;
            khoiTao_DSHDD();
        }

        private void khoiTao_HDD()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = string.Format("{0}Resources\\DonGiaDien.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
            string donGiaDien;
            using (StreamReader sr = new StreamReader(FileName))
            {
                donGiaDien = sr.ReadToEnd();
            }
            cboThang_ThemHDD.Items.Clear();
            cboPhong_ThemHDD.Items.Clear();
            txtDonGia_HDD.Text = donGiaDien;
            txtSoDoDien_HDD.Text = "";
            dtpNgayLap_HDD.Value = DateTime.Now;
            List<Phong> dsPhong = db.Phongs.ToList();
            for (int i = 1; i <= 12; i++)
            {
                cboThang_ThemHDD.Items.Add(i);
            }
            cboThang_ThemHDD.SelectedIndex = cboThang_ThemHDD.Items.IndexOf(DateTime.Now.Month);
            txtNam_ThemHDD.Text = DateTime.Now.Year.ToString();
            foreach (var x in dsPhong)
            {
                cboPhong_ThemHDD.Items.Add(x);
            }
            cboPhong_ThemHDD.SelectedIndex = 0;
            Phong p = (Phong)cboPhong_ThemHDD.SelectedItem;
            lbSoDo_ThemHDD.Text = p.SoDien.ToString();
        }

        private void khoiTao_DSHDD()
        {
            txtMaHD_DSHDD.Text = "";
            txtMaQL_DSHDD.Text = "";
            cboMaPhong_DSHDD.Items.Clear();
            List<Phong> dsPhong = db.Phongs.ToList();
            foreach (var x in dsPhong)
            {
                cboMaPhong_DSHDD.Items.Add(x);
            }
            HienThi_DSHDD(db.HoaDonDiens.ToList());
        }

        private void HienThi_DSHDD(List<HoaDonDien> dsHDD)
        {
            dataDSHDD.Rows.Clear();
            List<Phong> dsphong = db.Phongs.ToList();
            foreach (var i in dsHDD)
            {
                dataDSHDD.Rows.Add(new object[]
                {
                    i.MaHoaDonDien,
                    i.MaQL,
                    dsphong.Find(a=>a.MaPhong==i.MaPhong).TenPhong,
                    i.Thang,
                    i.Nam,
                    i.SoDienSuDung,
                    i.DonGiaDien,
                    i.SoDienSuDung*i.DonGiaDien,
					i.NgayLap.ToShortDateString(),
                    i.TinhTrang?"Đã thanh toán":"Nợ"
                });
            }
        }
		
		private void KhoiTao_HDP()
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = string.Format("{0}Resources\\DonGiaPhong.txt", Path.GetFullPath(Path.Combine(RunningPath, @"..\..\")));
            string donGiaPhong;
            using (StreamReader sr = new StreamReader(FileName))
            {
                donGiaPhong=sr.ReadToEnd();
            }    
            cboQuy_ThemHDP.Items.Clear();
            txtMSSV_ThemHDP.Text = "";
            txtNam_ThemHDP.Text = DateTime.Now.Year.ToString();
            txtThanhTien_ThemHDP.Text = donGiaPhong;
            dtpNgayLap_ThemHDP.Value = DateTime.Now;
            for (int i = 1; i < 5; i++)
            {
                cboQuy_ThemHDP.Items.Add(i);
            }
            cboQuy_ThemHDP.SelectedIndex = 0;

            if (DateTime.Now.Month >= 1 || DateTime.Now.Month <= 3)
                cboQuy_ThemHDP.SelectedIndex = cboQuy_ThemHDP.Items.IndexOf(1);
            if (DateTime.Now.Month >= 4 || DateTime.Now.Month <= 6)
                cboQuy_ThemHDP.SelectedIndex = cboQuy_ThemHDP.Items.IndexOf(2);
            if (DateTime.Now.Month >= 7 || DateTime.Now.Month <= 9)
                cboQuy_ThemHDP.SelectedIndex = cboQuy_ThemHDP.Items.IndexOf(3);
            if (DateTime.Now.Month >= 10 || DateTime.Now.Month <= 12)
                cboQuy_ThemHDP.SelectedIndex = cboQuy_ThemHDP.Items.IndexOf(4);
        }

        private void KhoiTao_DSHDP()
        {
            txtMaHD_DSHDP.Text = "";
            txtMaQL_DSHDP.Text = "";
            txtMSSV_DSHDP.Text = "";
            HienThi_DSHDP(db.HoaDonTienPhongs.ToList());
        }

        private void HienThi_DSHDP(List<HoaDonTienPhong> dsHDP)
        {
            dataDSHDP.Rows.Clear();
            foreach (var i in dsHDP)
            {
                dataDSHDP.Rows.Add(new object[]
                {
                    i.MaHoaDonTP,
                    i.MaQL,
                    i.MSSV,
                    i.Quy,
                    i.Nam,
                    i.ThanhTien,
                    i.NgayLap.ToShortDateString(),
                    i.TinhTrang?"Đã thanh toán":"Nợ"
                });
            }
        }

        public List<HoaDonDien> timHDDTheoPhong(String maphong)
        {
            List<HoaDonDien> dshdd = new List<HoaDonDien>();
            foreach (HoaDonDien hdd in db.HoaDonDiens)
            {
                if (hdd.MaPhong == maphong)
                    dshdd.Add(hdd);
            }
            return dshdd;
        }

        public List<HoaDonDien> timHDDTheoMaQL(String maql)
        {
            List<HoaDonDien> dshdd = new List<HoaDonDien>();
            foreach (HoaDonDien hdd in db.HoaDonDiens)
            {
                if (hdd.MaQL == maql)
                    dshdd.Add(hdd);
            }
            return dshdd;
        }

        public List<HoaDonDien> timHDDTheoMaQLvaPhong(String maql, String maphong)
        {
            List<HoaDonDien> dshdd = new List<HoaDonDien>();
            foreach (HoaDonDien hdd in db.HoaDonDiens)
            {
                if (hdd.MaQL == maql && hdd.MaPhong == maphong)
                    dshdd.Add(hdd);
            }
            return dshdd;
        }

        private void btnTimKiem_DSHDD_Click(object sender, EventArgs e)
        {
            List<Phong> phongs = db.Phongs.ToList();
            Phong p = (Phong)cboMaPhong_DSHDD.SelectedItem;
            String maphong = null;
            if (p != null)
            {
                maphong = phongs.Find(a => a.TenPhong == p.TenPhong).MaPhong;
            }
            String maql = txtMaQL_DSHDD.Text;

            if (txtMaQL_DSHDD.Text == "")
            {
                HienThi_DSHDD(timHDDTheoPhong(maphong));
            }
            else if (maphong == null)
            {
                HienThi_DSHDD(timHDDTheoMaQL(maql));
            }
            else
                HienThi_DSHDD(timHDDTheoMaQLvaPhong(maql, maphong));
        }

        private void btnDSHDD_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpDSHDD;
            khoiTao_DSHDD();
        }

        private void btnSua_DSHDD_Click(object sender, EventArgs e)
        {
            int Index = dataDSHDD.CurrentCell.RowIndex;
            int maHD = (int)dataDSHDD.Rows[Index].Cells[0].Value;
            HoaDonDien hdd = db.HoaDonDiens.Find(maHD);
            txtMaHD_SuaHDD.Text = hdd.MaHoaDonDien.ToString();
            txtDonGia_SuaHDD.Text = hdd.DonGiaDien.ToString();
            txtSoDoDien_SuaHDD.Text = hdd.SoDienSuDung.ToString();
            dtpNgayLap_HDD.Value = hdd.NgayLap;
            if (hdd.TinhTrang.ToString().Equals("True"))
            {
                rdbDaThanhToan_SuaHDD.Checked = true;
            }
            else
            {
                rdbNo_SuaHDD.Checked = true;
            }

            foreach (var x in db.Phongs)
            {
                cboPhong_SuaHDD.Items.Add(x);
            }

            String phong = hdd.Phong.MaPhong.ToString();
            int i = 0;
            for (int j = 0; j < db.Phongs.Count(); j++)
            {
                if (db.Phongs.ToList()[j].MaPhong == phong)
                    i = j;
            }
            cboPhong_SuaHDD.SelectedIndex = i;
            tabControlKTX.SelectedTab = tpSuaHDD;
        }

        private void btnXoa_DSHDD_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Không thể xóa Hóa đơn điện", "Không xóa được", MessageBoxButtons.OK);
        }

        private void btnTatCa_DSHDD_Click(object sender, EventArgs e)
        {
            cboMaPhong_DSHDD.SelectedIndex = -1;
            khoiTao_DSHDD();
        }

        private void btnQuayLai_SuaHDD_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpDSHDD;
            khoiTao_DSHDD();
        }

        private void btnSua_SuaHDD_Click(object sender, EventArgs e)
        {
            HoaDonDien hdd = db.HoaDonDiens.Find(int.Parse(txtMaHD_SuaHDD.Text));
            hdd.NgayLap = dtpNgayLap_SuaHDD.Value;
            hdd.SoDienSuDung = int.Parse(txtSoDoDien_SuaHDD.Text.ToString());
            hdd.Phong = (Phong)cboPhong_SuaHDD.SelectedItem;
            hdd.DonGiaDien = decimal.Parse(txtDonGia_SuaHDD.Text.ToString());
            if (rdbNo_SuaHDD.Checked == true)
            {
                hdd.TinhTrang = false;
            }
            else
                hdd.TinhTrang = true;
            db.SaveChanges();
            tabControlKTX.SelectedTab = tpDSHDD;
            khoiTao_DSHDD();
        }

        private void btnThem_HDD_Click(object sender, EventArgs e)
        {
            if (txtSoDoDien_HDD.Text == "" || txtDonGia_HDD.Text == "" || txtNam_ThemHDD.Text == "")
            {
                MessageBox.Show("Không được để trống", "Không thêm được", MessageBoxButtons.OK);
                return;
            }
            Phong p = (Phong)cboPhong_ThemHDD.SelectedItem;
            HoaDonDien hdd = new HoaDonDien();
            if (int.Parse(txtSoDoDien_HDD.Text) <= p.SoDien)
            {
                MessageBox.Show("Phải nhập số đo điện lớn hơn số điện phòng", "Không thêm được", MessageBoxButtons.OK);
                return;
            }

            hdd.MaQL = maQL;
            hdd.MaPhong = p.MaPhong;
            hdd.SoDienSuDung = int.Parse(txtSoDoDien_HDD.Text) - (int)p.SoDien;
            hdd.DonGiaDien = int.Parse(txtDonGia_HDD.Text);
            hdd.NgayLap = dtpNgayLap_HDD.Value;
            hdd.Thang = (int)cboThang_ThemHDD.SelectedItem;
            hdd.Nam = int.Parse(txtNam_ThemHDD.Text.ToString());
            if (db.HoaDonDiens.Where(x => (x.Nam == hdd.Nam) && (x.Thang == hdd.Thang) && (x.MaPhong == hdd.MaPhong)).Count() > 0)
            {
                MessageBox.Show("Đã tồn tại hóa đơn của phòng trong tháng này!");
                return;
            }
            p.SoDien = int.Parse(txtSoDoDien_HDD.Text);
            db.HoaDonDiens.Add(hdd);
            db.SaveChanges();

            khoiTao_HDD();           
            tabControlKTX.SelectedTab = tpDSHDD;
            khoiTao_DSHDD();
		}

        private void btnThem_ThemHDP_Click(object sender, EventArgs e)
        {
            if (txtNam_ThemHDP.Text == "" || txtThanhTien_ThemHDP.Text == "" || txtMSSV_ThemHDP.Text == "")
            {
                MessageBox.Show("Không được để trống", "Không thêm được", MessageBoxButtons.OK);
            }
            else
            {
                int quy = int.Parse(cboQuy_ThemHDP.SelectedItem.ToString());
                HoaDonTienPhong hdp = new HoaDonTienPhong();
                hdp.MaQL = maQL;
                hdp.Quy = quy;
                hdp.MSSV = txtMSSV_ThemHDP.Text;
                hdp.Nam = int.Parse(txtNam_ThemHDP.Text);
                hdp.ThanhTien = decimal.Parse(txtThanhTien_ThemHDP.Text);
                hdp.NgayLap = dtpNgayLap_ThemHDP.Value;
                db.HoaDonTienPhongs.Add(hdp);
                db.SaveChanges();

                KhoiTao_HDP();               
                tabControlKTX.SelectedTab = tpDSHDP;
                KhoiTao_DSHDP();

            }
        }

        private void btnTim_DSHDP_Click(object sender, EventArgs e)
        {

        }

        private void btnSua_DSHDP_Click(object sender, EventArgs e)
        {
            int Index = dataDSHDP.CurrentCell.RowIndex;          
            int maHD = (int)dataDSHDP.Rows[Index].Cells[0].Value;
            HoaDonTienPhong hdp = db.HoaDonTienPhongs.Find(maHD);
            txtMaHD_SuaHDP.Text = hdp.MaHoaDonTP.ToString();
            txtMSSV_SuaHDP.Text = hdp.MSSV.ToString();
            txtNam_SuaHDP.Text = hdp.Nam.ToString();
            txtThanhTien_SuaHDP.Text = hdp.ThanhTien.ToString();
            dtpNgayLap_SuaHDP.Value = hdp.NgayLap;
            if (hdp.TinhTrang.ToString().Equals("True"))
            {
                rdbDaThanhToan_SuaHDP.Checked = true;
            }
            else
                rdbNo_SuaHDP.Checked = true;

            for (int j = 1; j < 5; j++)
            {
                cboQuy_SuaHDP.Items.Add(j);
            }

            cboQuy_SuaHDP.SelectedIndex = cboQuy_SuaHDP.Items.IndexOf(hdp.Quy);
            tabControlKTX.SelectedTab = tpSuaHDP;
        }

        private void btnXoa_DSHDP_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Không thể xóa Hóa tền phòng", "Không xóa được", MessageBoxButtons.OK);
        }

        private void btnTatCa_DSHDP_Click(object sender, EventArgs e)
        {

        }

        private void btnQuayLai_SuaHDP_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpDSHDP;
            KhoiTao_DSHDP();
        }

        private void btnSua_SuaHDP_Click(object sender, EventArgs e)
        {
            HoaDonTienPhong hdp = db.HoaDonTienPhongs.Find(int.Parse(txtMaHD_SuaHDP.Text));
            hdp.NgayLap = dtpNgayLap_SuaHDP.Value;
            hdp.MSSV = txtMSSV_SuaHDP.Text.ToString();
            hdp.Nam = int.Parse(txtNam_SuaHDP.Text.ToString());
            hdp.ThanhTien = decimal.Parse(txtThanhTien_SuaHDP.Text.ToString());
            hdp.Quy = int.Parse(cboQuy_SuaHDP.SelectedItem.ToString());
            if (rdbNo_SuaHDP.Checked == true)
            {
                hdp.TinhTrang = false;
            }
            else
                hdp.TinhTrang = true;
            db.SaveChanges();
            tabControlKTX.SelectedTab = tpDSHDP;
            KhoiTao_DSHDP();

        }
    }
}
