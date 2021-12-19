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
    }
}
