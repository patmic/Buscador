using System.Net;
using System.Net.Mail;
using WebApp.Service.IService;

namespace WebApp.Service {
    public class EmailService (IConfiguration configuration, ILogger<EmailService> logger) : IEmailService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<EmailService> _logger = logger;

        public async Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpo)
        {
            var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_configuration["EmailSettings:Port"]),
                Credentials = new NetworkCredential(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]),
                EnableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:From"]),
                Subject = asunto,
                Body = cuerpo,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(destinatario);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Correo enviado a {0}", destinatario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enviando correo a {0}", destinatario);
            }
        }
    }
}