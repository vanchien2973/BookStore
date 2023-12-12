using BookStore.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BookStore.Services;

public class EmailSender : IEmailSender
{
    private readonly ISendGridClient _sendGridClient;
    private readonly SendGridSettings _sendGridSettings;

    public EmailSender(ISendGridClient sendGridClient, IOptions<SendGridSettings> sendGridSettings)
    {
        _sendGridClient = sendGridClient;
        _sendGridSettings = sendGridSettings.Value;   
    }
        
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(_sendGridSettings.FromEmail, _sendGridSettings.EmailName),
            Subject = subject,
            HtmlContent = htmlMessage
        };
        msg.AddTo(email);
        await _sendGridClient.SendEmailAsync(msg);
    }
}