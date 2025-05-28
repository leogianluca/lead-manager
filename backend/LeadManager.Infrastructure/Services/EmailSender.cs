using LeadManager.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    private readonly string _emailFolder;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
        _emailFolder = Path.Combine(AppContext.BaseDirectory, "Emails");

        try
        {
            if (!Directory.Exists(_emailFolder))
                Directory.CreateDirectory(_emailFolder);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar diretório para emails falsos.");
        }
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            var fileName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{Guid.NewGuid()}.txt";
            var filePath = Path.Combine(_emailFolder, fileName);

            var content = $"To: {to}\nSubject: {subject}\n\n{body}";

            await File.WriteAllTextAsync(filePath, content);

            _logger.LogInformation("Email enviado para {Email} com assunto {Subject}", to, subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar email para {Email}", to);
        }
    }
}
