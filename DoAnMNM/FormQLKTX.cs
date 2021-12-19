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
    }
}
