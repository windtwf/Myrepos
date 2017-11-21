using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class SendEmailTest
    {
        #region Method - SendEmail

        public static void SendEmail(string body, string environment)
        {
            try
            {
                using (TextWriter file = File.CreateText(DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss") + ".htm"))
                {
                    file.Write(body);
                }

                bool sendEmail = Convert.ToBoolean(ConfigurationManager.AppSettings["SendEmail"]);
                if (!sendEmail)
                    return;

                string subject = "[" + environment + "]" + ConfigurationManager.AppSettings["Subject"] + "-" + DateTime.Now;
                string fromAddress = ConfigurationManager.AppSettings["FromEmail"];
                string toAddress = ConfigurationManager.AppSettings["ToEmail"];
                string attachmentFile = AppDomain.CurrentDomain.BaseDirectory + @"\First.xml";

                for (int i = 1; i < 4; i++)
                {
                    Console.WriteLine("email try " + i);
                    bool status = Send(i, fromAddress, toAddress, subject, body, attachmentFile);
                    if (status)
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static bool Send(int count, string fromAddress, string toAddress, string subject, string body, string attachmentFile)
        {
            bool status = false;
            try
            {
                Attachment attachment = new Attachment(attachmentFile);
                SendMail(fromAddress, toAddress, subject, body, attachment);
                status = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);

                Console.WriteLine("email fail count " + count);
                if (count == 3)
                    SendMail(fromAddress, toAddress, subject, body, null);
            }

            if (status)
                Console.WriteLine("Email gone.");
            return status;
        }

        public static void SendMail(string fromAddress, string toAddress, string subject, string body, Attachment attachment)
        {
            // 126 Email Smtp: smtp.126.com
            SmtpClient client = new SmtpClient("smtphost.redmond.corp.microsoft.com");
            client.UseDefaultCredentials = true;

            MailAddress from = new MailAddress(fromAddress);
            MailMessage message = new MailMessage();
            if (attachment != null)
                message.Attachments.Add(attachment);

            message.IsBodyHtml = true;
            message.From = from;

            if (toAddress.Contains(";"))
            {
                string[] toAddresses = toAddress.Split(';');
                foreach (string address in toAddresses)
                {
                    message.To.Add(address);
                }
            }
            else
            {
                message.To.Add(toAddress);
            }

            message.Body = body;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            client.Send(message);
            message.Dispose();
            message = null;
            client.Dispose();
            client = null;
        }

        #endregion
    }
}
