using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

namespace GornolignuiKypopt
{
    public partial class OtpravitPismo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btSend_Click(object sender, EventArgs e)
        {
            int port = 587;
            bool enableSSL = true;

            string emailFrom = "belo12345@bk.ru";
            string password = "botbot123";
            string emailTo = "i_a.k.belogrudov@mpt.ru";
            string smtpAddress = "smtp.mail.ru";
            string name = "Отправил пользователь " + tbName.Text.ToString() + ", почта " + tbMail.Text;
            string message = tbMessage.Text;

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(emailFrom);
            mail.To.Add(emailTo);
            mail.Subject = "Письмо обратной связи";
            mail.Body = "\r\n" + name + "\r\n" + "\r\n" + message;
            mail.IsBodyHtml = false;
            using (SmtpClient smtp = new SmtpClient(smtpAddress, port))
            {
                smtp.Credentials = new NetworkCredential(emailFrom, password);
                smtp.EnableSsl = enableSSL;
                smtp.Send(mail);
            }
            tbMail.Text = "";
            tbMessage.Text = "";
            tbName.Text = "";
        }
    }
}