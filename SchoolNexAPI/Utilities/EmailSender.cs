using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using SchoolNexAPI.DTOs;

namespace SchoolNexAPI.Utilities
{
    public class EmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task SendAsync(string to, string message, string v)
        {
            try
            {
                var emailConfig = _configuration.GetSection("EmailConfig");
                var emailData = new EmailSenderDto
                {
                    To = to,
                    Body = message+" "+v,
                    Subject = "SchoolNxt Notification",
                    From = emailConfig["Email"],
                    RecipientName = "User"
                };

                using (var client = new SmtpClient(emailConfig["Server"], Convert.ToInt32(emailConfig["Port"])))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(emailConfig["Email"], emailConfig["Password"]);

                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(emailData.From, "SchoolNxt");
                        mailMessage.To.Add(emailData.To);
                        mailMessage.Subject = emailData.Subject;
                        mailMessage.Body = emailData.Body;
                        mailMessage.IsBodyHtml = true;
                        client.Send(mailMessage);
                    }
                }

                await Task.CompletedTask; // Just to keep the async signature
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
