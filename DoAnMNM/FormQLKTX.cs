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
    }
}
