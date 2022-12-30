using MimeKit;

namespace UserControl.Models
{
    public class Message
    {
        public List<MailboxAddress> Recipients { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(string subject, IEnumerable<string> recipients, int userId, string activationCode)
        {

            Recipients = new List<MailboxAddress>();
            Recipients.AddRange(recipients.Select(x => new MailboxAddress(x)));
            Subject = subject;

            Content = $"Seu link de ativação: https://localhost:1010/RegisterUser/ActivateUserAccount?UserId={userId}&ActivationCode={activationCode}"; 
        }
    }
}
