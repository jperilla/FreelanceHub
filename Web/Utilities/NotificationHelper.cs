using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Web.Models;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;

namespace Web.Utilities
{
    public class NotificationHelper
    {
        public static bool SendPasswordRetrieval(string emailAddress, ControllerContext ctx)
        {
            try
            {
                StringBuilder emailMessage = new StringBuilder();

                emailMessage.Append("<br />");
                emailMessage.Append("Hello,");
                emailMessage.Append("You have requested a password recovery for your Freelance Hub account.");
                emailMessage.Append("<br />");
                emailMessage.Append("Please click the link below to change your password: <br />");
                emailMessage.Append("<br />");
                emailMessage.Append(string.Format(Account.APP_BASE_URL_STATIC + "/Account/ResetPassword?email={0}", emailAddress));
                emailMessage.Append("<br />");

                MailMessage email = new MailMessage();
                email.From = new MailAddress("info@geekgirlsoftware.com");
                email.To.Add(new MailAddress(emailAddress));
                email.Subject = "Freelance Hub Password Recovery";
                email.Body = emailMessage.ToString();
                email.IsBodyHtml = true;

                SmtpClient smtpServer = new SmtpClient();
                smtpServer.Host = "smtp.gmail.com";
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential("julie@geekgirlsoftware.com", "M!ckey16!");
                smtpServer.EnableSsl = true;
                smtpServer.Send(email);
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(String.Format(e.Message + ": Failure to send email to {0}.", emailAddress));
                return false;
            }
        }
    }
}