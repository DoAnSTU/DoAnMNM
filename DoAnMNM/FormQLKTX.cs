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
            KhoiTao_ThongKe();
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

        private void button1_Click(object sender, EventArgs e)
        {
            tabControlKTX.SelectedTab = tpThongKe;
            KhoiTao_ThongKe();
        }

        private void KhoiTao_ThongKe()
        {
            lbSoPhong_ThongKe.Text = db.Phongs.Count().ToString();
            lbSoSV_ThongKe.Text = db.SinhViens.Count().ToString();
            lbSoGiuong_ThongKe.Text = tinhTongGiuongDaO().ToString() + "/" + tinhTongGiuong().ToString();
            cboQuy_ThongKe.Items.Clear();
            for (int i = 1; i <= 4; i++)
            {
                cboQuy_ThongKe.Items.Add(i);
            }
            cboQuy_ThongKe.SelectedIndex = 0;
            if (DateTime.Now.Month >= 1 || DateTime.Now.Month <= 3)
                cboQuy_ThongKe.SelectedIndex = cboQuy_ThongKe.Items.IndexOf(1);
            if (DateTime.Now.Month >= 4 || DateTime.Now.Month <= 6)
                cboQuy_ThongKe.SelectedIndex = cboQuy_ThongKe.Items.IndexOf(2);
            if (DateTime.Now.Month >= 7 || DateTime.Now.Month <= 9)
                cboQuy_ThongKe.SelectedIndex = cboQuy_ThongKe.Items.IndexOf(3);
            if (DateTime.Now.Month >= 10 || DateTime.Now.Month <= 12)
                cboQuy_ThongKe.SelectedIndex = cboQuy_ThongKe.Items.IndexOf(4);
            txtNam_ThongKe.Text = DateTime.Now.Year.ToString();
            cboThang_ThongKe.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                cboThang_ThongKe.Items.Add(i);
            }
            cboThang_ThongKe.SelectedIndex = cboThang_ThongKe.Items.IndexOf(DateTime.Now.Month);
            txtNam2_ThongKe.Text = DateTime.Now.Year.ToString();
            HienThi_ChartTienDien((int)cboThang_ThongKe.SelectedItem, int.Parse(txtNam2_ThongKe.Text));
            HienThi_ChartTienPhong((int)cboQuy_ThongKe.SelectedItem, int.Parse(txtNam_ThongKe.Text));
        }

        private int tinhTongGiuongDaO()
        {
            int sum = 0;
            foreach (var x in db.Phongs)
            {
                sum += timSoSinhVienTrongPhong(x);
            }
            return sum;
        }

        private int tinhTongGiuong()
        {
            int sum = 0;
            foreach (var x in db.Phongs)
            {
                sum += x.LoaiPhong.TongSoGiuong;
            }
            return sum;
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

        private void HienThi_ChartTienDien(int thang, int nam)
        {

            //danh sách hóa đơn có tháng và năm như biến
            List<HoaDonDien> ds = db.HoaDonDiens.Where(a => (a.Thang == thang) && (a.Nam == nam)).ToList();
            int soHDNo = 0;
            foreach (var x in ds)
            {
                if (x.TinhTrang == false)
                {
                    soHDNo++;
                }
            }
            chartTienDien_ThongKe.Series["s1"].Points.AddXY("Nợ", soHDNo);
            chartTienDien_ThongKe.Series["s1"].Points.AddXY("Thanh toán", ds.Count() - soHDNo);
        }

        private void HienThi_ChartTienPhong(int quy, int nam)
        {
            List<HoaDonTienPhong> ds = db.HoaDonTienPhongs.Where(a => (a.Quy == quy) && (a.Nam == nam)).ToList();
            int soHDNo = 0;
            foreach (var x in ds)
            {
                if (x.TinhTrang == false)
                {
                    soHDNo++;
                }
            }
            chartTienPhong_ThongKe.Series["s1"].Points.AddXY("Nợ", soHDNo);
            chartTienPhong_ThongKe.Series["s1"].Points.AddXY("Thanh toán", ds.Count() - soHDNo);
        }
    }
}
