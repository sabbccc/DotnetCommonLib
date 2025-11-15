namespace Common.Core.Services.Sms
{
    public interface ISmsService
    {
        Task SendSmsAsync(string to, string message);
    }
}
