
using BookingClone.Application.Contracts;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace BookingClone.Infrastructure.Services;
public class EmailService : IEmailService
{
    private readonly IConfiguration configuration;
    private readonly ILogger<EmailService> logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        this.configuration = configuration;
        this.logger = logger;
    }

    private  MimeMessage GetMimeMsg(string From, string To, string Subject, string Body)
    {
        MimeMessage mimeMessage = new MimeMessage();

        mimeMessage.From.Add(new MailboxAddress("Mohamed Abdelaziz", From));
        mimeMessage.To.Add(new MailboxAddress(To, To));
        mimeMessage.Subject = Subject;
        mimeMessage.Body = new TextPart(TextFormat.Html) { Text = Body };

        return mimeMessage;
    }

    public async Task<bool> SendMail(string To, string Subject, string Body)
    {

        string host = configuration["Smtp:Host"]!;
        int port = int.Parse(configuration["Smtp:Port"]!);

        string userName = configuration["Smtp:Username"]!;
        string password = configuration["Smtp:Password"]!;

        string From = configuration["Smtp:Username"]!;

        using (var SmtpClient = new SmtpClient())
        {
            try
            {
                await SmtpClient.ConnectAsync(host, (int)port, SecureSocketOptions.StartTls);
                SmtpClient.Authenticate(userName, password);
                await SmtpClient.SendAsync(this.GetMimeMsg(From, To, Subject, Body));
                await SmtpClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
            
        }

        return true;
    }
}
