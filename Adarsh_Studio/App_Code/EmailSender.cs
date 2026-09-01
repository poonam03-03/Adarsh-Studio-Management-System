using System.Net;
using System.Net.Mail;
using Microsoft.Identity.Client;

namespace Adarsh_Studio.App_Code
{
    public class EmailSender
    {
        private string MyEmailId;
        private string MyEmailAppPass;
        public EmailSender()
        {
            // MyEmailId = config.GetSection("Email").GetSection("MyEmail").Value;
            //MyEmailAppPass = config.GetSection("Appcode").GetSection("AppCode").Value;
            MyEmailId = "pradeepdohare516@gmail.com";
            MyEmailAppPass = "xrwbtmespczvlyib";

        }


        internal bool SendEmailNow(string SendTo, string Subject, string Message)
        {
            try
            {
                //setting  credentials...
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                NetworkCredential MyCred = new NetworkCredential(MyEmailId, MyEmailAppPass);
                client.Credentials = MyCred;
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                //setting mail message
                MailMessage msg = new MailMessage();
                MailAddress MaFrom = new MailAddress(MyEmailId, "Example Project");
                msg.From = MaFrom;
                msg.To.Add(SendTo);
                msg.Subject = Subject;
                msg.Body = Message;
                msg.Sender = MaFrom;
                //Adding signature in message of email.
                Message = Message + "\n\nFrom- Adarsh Studio\n www.adarshstdio.com";
                msg.Body = Message;
                //sending mail
                client.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Email Sending: " + ex.Message);
                return false;
            }
        }
    }

}
