using Usuarios.Application.Abstractions.Email;

namespace Usuarios.Infrastructure.abstractions.email;

internal sealed class EmailService : IEmailService
{
    public Task SendEmailAsync(string to, string subject, string body)
    {
        return Task.CompletedTask;
    }
}