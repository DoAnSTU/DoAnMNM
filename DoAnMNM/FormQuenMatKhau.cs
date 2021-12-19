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
using System.Threading;
using DoAnMNM.Models;

namespace DoAnMNM
{
    public partial class FormQuenMatKhau : Form
    {
        ModelQLKTX db = new ModelQLKTX();
        public FormQuenMatKhau()
        {
            InitializeComponent();
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            QuanLy ql = db.QuanLies.ToList().Find(a => a.DiaChi == txtEmail.Text);
            if (ql != null)
            {
                FormDoi f = new FormDoi();
                f.Visible = true;
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress("duonghiep.doan@gmail.com");
                    message.To.Add(new MailAddress(ql.DiaChi));
                    message.Subject = "Tài khoản Mật khẩu " + (DateTime.Now).ToString();
                    message.Body = "Tài khoản là: " + ql.MaQL + " || Mật khẩu là: " + ql.MatKhau;
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com"; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("duonghiep.doan@gmail.com", "Doan1234");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    f.Visible = false;
                    MessageBox.Show("Gửi thành công, kiểm tra Email");
                }
                catch (Exception ex)
                {
                    f.Visible = false;
                    MessageBox.Show("Gửi thất bại");
                }
            }
            else
            {
                MessageBox.Show("Không tồn tại tài khoản");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
