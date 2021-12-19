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
    }
}
