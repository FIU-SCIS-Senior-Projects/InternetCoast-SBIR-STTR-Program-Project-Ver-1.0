using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using Elmah;
using InternetCoast.Infrastructure.Diagnostics;

namespace InternetCoast.Infrastructure.Email
{
    public class EmailGenerator
    {
        public static string BuildEmailBody(string templateName, IDictionary<string, string> keyValues)
        {
            var body = new StringBuilder(
                File.ReadAllText(
                HttpContext.Current.Server.MapPath(
                        "~/EmailTemplates/" + templateName + ".html"
                    )
                )
            );

            foreach (var key in keyValues.Keys)
            {
                body.Replace(key, keyValues[key]);
            }

            return body.ToString();
        }

        public static bool SendEmail(string sender, string receiver, string title, string templateName, IDictionary<string, string> templateParameters, string[] attachments = null)
        {
            var body = BuildEmailBody(templateName, templateParameters);

            var mailMessage = new MailMessage();
            var multi = receiver.Split(';');
            foreach (var address in multi)
            {
                mailMessage.To.Add(new MailAddress(address));
            }

            mailMessage.From = new MailAddress(sender);
            mailMessage.Subject = title;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            if (attachments != null && attachments.Any())
            {
                foreach (var a in attachments)
                {
                    var file = HttpContext.Current.Server.MapPath("~/Content/PDF/" + a);
                    
                    var data = new Attachment(file, MediaTypeNames.Application.Octet);
                    // Add time stamp information for the file.
                    var disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(file);
                    disposition.ModificationDate = File.GetLastWriteTime(file);
                    disposition.ReadDate = File.GetLastAccessTime(file);
                    // Add the file attachment to this e-mail message.
                    mailMessage.Attachments.Add(data);
                }
            }

            var smtp = new SmtpClient();
            try
            {
                smtp.Send(mailMessage);
                var msg = $"Email sent to: {receiver}, Template name: {templateName}";
                Trace.WriteLine(msg);
            }
            catch (Exception e)
            {
                var msg = $"Unable to send email to: {receiver}. Template name: {templateName}. Time recorded: {DateTime.Now}";
                var ex = new CustomEmailException(msg, e);
                ErrorSignal.FromCurrentContext().Raise(ex);
                return false;
            }

            return true;
        }
    }
}