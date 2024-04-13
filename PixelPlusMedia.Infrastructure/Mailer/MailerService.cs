using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using PixelPlusMedia.Application.Contracts.Infrastructure;
using PixelPlusMedia.Application.Models.Mailer;
using MailKit.Net.Smtp;

namespace PixelPlusMedia.Infrastructure.Mailer;

public class MailerService : IEmailService
{
    public EmailSettings _emailSettings { get; }

    public MailerService(IOptions<EmailSettings> mailSettings)
    {
        _emailSettings = mailSettings.Value;
    }
    public bool SendEmail(Email email)
    {
        try
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_emailSettings.FromAddress));
            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.Subject = email.Subject;
            mail.Body = new TextPart(TextFormat.Html) { Text = email.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.SMTPHost, _emailSettings.SMTPPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.SMTPUser, _emailSettings.SMTPPassword);
            smtp.Send(mail);
            smtp.Disconnect(true);

            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

}
