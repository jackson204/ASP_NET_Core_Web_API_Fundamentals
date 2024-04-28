namespace CityInfo.API.Services;

public class LocalMailService : ILocalMailService
{
    public string _mailFrom;
    public string _mailTo;

    private IConfiguration _configuration;

    public LocalMailService(IConfiguration configuration)
    {
        _mailFrom = configuration["mailSettings:mailFrom"];
        _mailTo = configuration["mailSettings:mailTo"];
    }

    public void Send(string subject, string message)
    {
        // send mail - output to debug window
        Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with {nameof(LocalMailService)}.");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Message: {message}");
    }
}
