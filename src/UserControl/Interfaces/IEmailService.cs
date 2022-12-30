using MimeKit;
using UserControl.Models;

namespace UserControl.Interfaces
{
    public interface IEmailService
    {
        bool SendEmailToConfirmAccount(string[] recipients, string subject, int userId, string activationCode);
        bool SendEmail(MimeMessage bodyMessage);

        MimeMessage CreateBodyMessage(Message message);

    }
}