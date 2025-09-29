using RentManagement.Api.Interfaces;
using System.Net;
using System.Net.Mail;

namespace RentManagement.Api.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly string _senderEmail;
        private readonly string _senderPassword;
        private readonly string _smtpServer;
        private readonly int _smtpPort;

        public SmtpEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _senderEmail = _configuration["EmailSettings:SenderEmail"] ?? string.Empty;
            _senderPassword = _configuration["EmailSettings:SenderPassword"] ?? string.Empty;
            _smtpServer = _configuration["EmailSettings:SmtpServer"] ?? string.Empty;
            _smtpPort = _configuration.GetValue<int>("EmailSettings:SmtpPort");
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = new MailMessage(_senderEmail, email)
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            using var smtpClient = new SmtpClient(_smtpServer, _smtpPort)
            {
                Credentials = new NetworkCredential(_senderEmail, _senderPassword),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mail);
        }
    }
}
