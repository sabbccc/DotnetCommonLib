using Common.Core.Configuration;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Common.Core.Services.Sms
{
    public class TwilioSmsService : ISmsService
    {
        private readonly SmsSettings _settings;

        public TwilioSmsService(IOptions<SmsSettings> options)
        {
            _settings = options.Value;
            TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);
        }

        public async Task SendSmsAsync(string to, string message)
        {
            await MessageResource.CreateAsync(
                to: new Twilio.Types.PhoneNumber(to),
                from: new Twilio.Types.PhoneNumber(_settings.FromNumber),
                body: message
            );
        }
    }
}
