using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
        Mock<IEmailService> mockEmailService;
        Mock<IConfiguration> configuration;
        Mock<IConfigurationSection> mockSection;


        public EmailServiceTests()
        {

            mockSection = new Mock<IConfigurationSection>();

            mockSection.Setup(x => x.Value).Returns("projetousercontrol@gmail.com");

            configuration = new Mock<IConfiguration>();

            mockEmailService = new Mock<IEmailService>();

            var a = configuration.Setup(x => x.GetSection(It.Is<string>(k => k == "EmailSettings:From"))).Returns(mockSection.Object);


            emailService = new EmailService(configuration.Object);
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
            var result = emailService.SendEmailToConfirmAccount(new string[] {}, "Tema", 1, "activationCode");

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
