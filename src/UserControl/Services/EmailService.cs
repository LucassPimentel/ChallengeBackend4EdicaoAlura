using MailKit.Net.Smtp;
using MimeKit;
using UserControl.Interfaces;
using UserControl.Models;


namespace UserControl.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmailToConfirmAccount(string[] recipients, string subject, int userId, string activationCode)
        {
            var message = new Message(subject, recipients, userId, activationCode);

            var bodyMessage = CreateBodyMessage(message);

            var isSent = SendEmail(bodyMessage);

            if (isSent)
            {
                return true;
            }
            return false;
        }

        public bool SendEmail(MimeMessage bodyMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate("projetousercontrol@gmail.com", "ydhrogeobwktzpuh");

                    client.Send(bodyMessage);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        public MimeMessage CreateBodyMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:From")));

            emailMessage.To.AddRange(message.Recipients);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = message.Content
            };

            return emailMessage;
        }
    }
}
