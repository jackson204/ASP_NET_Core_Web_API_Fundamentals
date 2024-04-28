namespace CityInfo.API.Services;

public class CloudMainService:ILocalMailService
{
    public string _mailFrom;
    public string _mailTo;

    private IConfiguration _configuration;

    public CloudMainService(IConfiguration configuration)
    {
        _mailFrom = configuration["mailSettings:mailFrom"];
        _mailTo = configuration["mailSettings:mailTo"];
    }
    public void Send(string subject, string message)
    {
        // send mail - output to debug window
        Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with {nameof(CloudMainService)}.");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Message: {message}");
    }
}
