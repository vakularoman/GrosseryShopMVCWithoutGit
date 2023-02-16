namespace AquaPlayground.Services
{
    using System.Net;
    using System.Net.Mail;
    using AquaPlayground.Models;
    using AquaPlayground.Services.Interfaces;
    using Microsoft.Extensions.Options;

    public class GoogleEmailService : IEmailService
    {
        private readonly EmailConfigModel _emailConfigModel;

        public GoogleEmailService(IOptions<EmailConfigModel> emailConfigModel)
        {
            _emailConfigModel = emailConfigModel.Value;
        }

        public async Task SendResetPasswordEmailAsync(string to, string link)
        {
            using var message = new MailMessage(_emailConfigModel.SenderAddress, to);

            message.Subject = _emailConfigModel.ResetSubject;
            message.Body = link;
            message.IsBodyHtml = _emailConfigModel.IsBodyHTML;

            using var smtp = new SmtpClient();
            smtp.Host = _emailConfigModel.Host;
            smtp.EnableSsl = true;

            var cred = new NetworkCredential(_emailConfigModel.SenderAddress, _emailConfigModel.Password);
            smtp.UseDefaultCredentials = _emailConfigModel.UseDefaultCredentials;
            smtp.Credentials = cred;
            smtp.Port = _emailConfigModel.Port;

            await smtp.SendMailAsync(message);
        }
    }
}
