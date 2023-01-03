using System.Net;
using System.Net.Mail;
using ControllerBaseLib;

namespace CheckingEmailCode
{
    public enum CheckingEmailOperations
    {
        CheckEmail = 0
    }

    public class CheckingEmailCodeClass : ControllerBase<CheckingEmailOperations>
    {
        #region Fields

        SmtpClient smtpClient;

        Random r;

        MailAddress m_from;

        #endregion

        #region Ctor

        public CheckingEmailCodeClass(int port, string host, SmtpDeliveryMethod smtpDeliveryMethod, MailAddress from, bool enableSsl = true, bool useDefaultCredentials = true, NetworkCredential networkCredential = null)
        {
            r = new Random();

           m_from = from;

            //mailMessage = new MailMessage();

            smtpClient = new SmtpClient();

            //mailMessage.Body = r.Next(1000, 9999).ToString();

            //mailMessage.From = new MailAddress("wrestler000ua@gmail.com");

            //mailMessage.Subject = 

            //mailMessage.IsBodyHtml = true;

            smtpClient.Port = port;

            smtpClient.Host = host;

            smtpClient.EnableSsl = enableSsl;

            smtpClient.UseDefaultCredentials = useDefaultCredentials;

            if (!useDefaultCredentials)
            {
                smtpClient.Credentials = networkCredential;
            }

            smtpClient.DeliveryMethod = smtpDeliveryMethod;
        }

        #endregion

        #region Method

        public void CheckEmailCode(string email, bool isBodyHtml = true)
        {
            ExecuteFunc(CheckingEmailOperations.CheckEmail, () =>
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = m_from;

                mailMessage.Subject = "Email verification.";

                mailMessage.IsBodyHtml = isBodyHtml;

                var code = GenerateCode(6, 1, 10);

                string message = $"<p><i>Your verification code:</i></p> ";

                foreach (var item in code)
                {
                    message += item.ToString() + " ";
                }

                message += "To finish registration, please, input this code in our app.";

                mailMessage.Body = message;

                mailMessage.To.Add(new MailAddress(email));

                smtpClient.Send(mailMessage);

                return code;
            });

            //mailMessage.To.Add(new MailAddress("egorolinek@gmail.com"));

            //smtpClient.Send(mailMessage);
        }

        private int[] GenerateCode(int count, int start, int end)
        {
            int[] array = new int[count];

            for (int i = 0; i < count;i ++)
            {
                array[i] = r.Next(start, end);
            }

            return array;
        }

        #endregion
    }
}