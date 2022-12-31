using System.Net;
using System.Net.Mail;

namespace CheckingEmailCode
{
    public class CheckingEmailCode
    {
        #region Fields

        MailMessage mailMessage;

        SmtpClient smtpClient;

        string message;

        #endregion

        #region Ctor

        public CheckingEmailCode()
        {
            mailMessage = new MailMessage();

            smtpClient = new SmtpClient();

            mailMessage.Body = message;

            mailMessage.From = new MailAddress("wrestler000ua@gmail.com");

            mailMessage.To.Add(new MailAddress("egorolinek@gmail.com"));

            mailMessage.Subject = "Test";

            mailMessage.IsBodyHtml = true;

            message = "<font> Test message </font>";

            smtpClient.Port = 587;

            smtpClient.Host = "smtp.gmail.com";

            smtpClient.EnableSsl = true;

            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential("wrestler000ua@gmail.com", "qilazsonycggwzlg");

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        #endregion

        #region Method



        #endregion
    }
}