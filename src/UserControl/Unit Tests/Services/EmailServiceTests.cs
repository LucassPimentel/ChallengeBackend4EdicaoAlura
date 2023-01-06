using FluentAssertions;
using MimeKit;
using Moq;
using UserControl.Interfaces;
using UserControl.Models;
using UserControl.Services;
using Xunit;

namespace UserControl.Unit_Tests.Services
{
    public class EmailServiceTests
    {
        EmailService emailService;
        public EmailServiceTests()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .AddUserSecrets<Program>()
                .Build();

            emailService = new EmailService(configuration);

        }

        [Fact]
        public void CreateBodyMessage_WhenBodyMessageIsCreated_ShouldReturnAMimeMessage()
        {
            var message = new Message("Tema", new string[] { "teste@gmail.com" }, 1, "activationCode");

            var result = emailService.CreateBodyMessage(message);

            result.Should().NotBeNull();
            result.Should().BeOfType<MimeMessage>();
        }

        [Fact]
        public void SendEmailToConfirmAccount_WhenSuccefullyExecuted_ShouldReturnTrue()
        {
            var result = emailService.SendEmailToConfirmAccount(new string[] { "teste@gmail.com" }, "Tema", 1, "activationCode");

            result.Should().BeTrue();
        }

        [Fact]
        public void SendEmailToConfirmAccount_WhenExecutedIsFailed_ShouldReturnFalse()
        {
            var result = emailService.SendEmailToConfirmAccount(new string[] { }, "Tema", 1, "activationCode");

            result.Should().BeFalse();
        }

        [Fact]
        public void SendEmail_WhenSuccefullyExecuted_ShouldReturnTrue()
        {
            var message = new Message("Tema", new string[] { "teste@gmail.com" }, 1, "activationCode");

            var bodyMessage = emailService.CreateBodyMessage(message);

            var result = emailService.SendEmail(bodyMessage);

            result.Should().BeTrue();
        }

        [Fact]
        public void SendEmail__WhenExecutedIsFailed_ShouldReturnFalse()
        {
            var message = new Message("Tema", new string[] { }, 1, "activationCode");

            var bodyMessage = emailService.CreateBodyMessage(message);

            var result = emailService.SendEmail(bodyMessage);

            result.Should().BeFalse();
        }
    }
}
