using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BotswanaLifeTransactionsExtractor
{
    class EmailSender
    {
        public string SmtpIPAddress { get; set; }
        public int SmtpPort { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public void SendEmail(string fromAddress, string fileToAttach, IEnumerable<string> toAddresses)
        {
            var mailMessage = new MailMessage()
            {
                From = new MailAddress(fromAddress),
                Subject = Subject,
                Body = Body,
                IsBodyHtml = true
            };

            var smtpClient = new SmtpClient
            {
                Port = SmtpPort,
                Host = SmtpIPAddress,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
            };



            foreach (var emailAddress in toAddresses)
            {
                mailMessage.To.Add(emailAddress);
            }

            mailMessage.Attachments.Add(new Attachment(fileToAttach));

            smtpClient.Send(mailMessage);
        }
    }
}
