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
    }
}
