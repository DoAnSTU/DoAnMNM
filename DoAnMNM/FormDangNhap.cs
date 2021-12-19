using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using DoAnMNM.Models;


namespace DoAnMNM
{
    public partial class FormDangNhap : Form
    {
        ModelQLKTX db = new ModelQLKTX();
        public FormDangNhap()
        {
            InitializeComponent();
            this.ActiveControl = txtTaiKhoan;

        }


        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            List<QuanLy> dsQL = db.QuanLies.ToList();
            QuanLy ql = dsQL.Find(a => a.MaQL == txtTaiKhoan.Text);
            if (ql != null)
            {
                if (txtMauKhau.Text == ql.MatKhau)
                {
                    this.Visible = false;

                    FormQLKTX f = new FormQLKTX(ql.MaQL);
                    f.ShowDialog();
                    this.Visible = true;
                    txtTaiKhoan.Text = "";
                    txtMauKhau.Text = "";
                    db = new ModelQLKTX();
                }
                else
                {
                    MessageBox.Show("Sai mật khẩu!");
                }
            }
            else
            {
                MessageBox.Show("Tài khoản không tồn tại!");
            }
        }

        private void txtMauKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangNhap_Click(this, new EventArgs());
            }
        }

        private void llbQuenMatKhau_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Visible = false;
            FormQuenMatKhau f = new FormQuenMatKhau();
            f.ShowDialog();
            this.Visible = true;
            txtTaiKhoan.Text = "";
            txtMauKhau.Text = "";
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
