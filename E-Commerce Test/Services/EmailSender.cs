using Microsoft.AspNetCore.Identity.UI.Services;

namespace E_Commerce_Test.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            System.Diagnostics.Debug.WriteLine(email);
            return Task.CompletedTask;
        }
    }
}
