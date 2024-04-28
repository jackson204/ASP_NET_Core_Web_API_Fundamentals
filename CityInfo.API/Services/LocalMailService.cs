namespace CityInfo.API.Services;

public class LocalMailService : ILocalMailService
{
    public string _mailFrom = "nor@com.com";
    public string _mailTo = "admin@company.com";

    public void Send(string subject, string message)
    {
        // send mail - output to debug window
        Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with {nameof(LocalMailService)}.");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Message: {message}");
    }
}
