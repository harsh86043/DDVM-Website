﻿using DDVM_Website.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace DDVM_Website.Services
{
    public class EmailService : IEmailService
    {
            private readonly IConfiguration _configuration;

            public EmailService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task SendEmailAsync(string toEmail, string subject, string message)
            {
                var smtpClient = new SmtpClient
                {
                    Host = _configuration["Email:SmtpServer"],
                    Port = int.Parse(_configuration["Email:Port"]),
                    Credentials = new NetworkCredential(
                        _configuration["Email:Username"],
                        _configuration["Email:Password"]
                    ),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["Email:Username"]),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
    }
}