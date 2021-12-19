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


namespace DoAnMNM
{
    public partial class FormDangNhap : Form
    {
        //doi ten 
        public FormDangNhap()
        {
            InitializeComponent();
            this.ActiveControl = txtTaiKhoan;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {

        }
    }
}
