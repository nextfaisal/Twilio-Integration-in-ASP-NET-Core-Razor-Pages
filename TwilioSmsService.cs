namespace Twil
{
    using Microsoft.Extensions.Configuration;
    using Twilio;
    using Twilio.Rest.Api.V2010.Account;
    using Twilio.Types;

    public class TwilioSmsService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromNumber;
        private readonly string _whatsAppFromNumber;

        public TwilioSmsService(IConfiguration config)
        {
            _accountSid = GetRequiredConfigValue(config, "Twilio:AccountSID");
            _authToken = GetRequiredConfigValue(config, "Twilio:AuthToken");
            _fromNumber = GetRequiredConfigValue(config, "Twilio:FromPhoneNumber");
            _whatsAppFromNumber = NormalizeWhatsAppAddress(GetRequiredConfigValue(config, "Twilio:WhatsAppFromNumber"));
        }

        public async Task<string> SendSmsAsync(string toNumber, string message)
        {
            TwilioClient.Init(_accountSid, _authToken);

            var messageResult = await MessageResource.CreateAsync(
                to: new PhoneNumber(toNumber),
                from: new PhoneNumber(_fromNumber),
                body: message
            );

            return messageResult.Sid;
        }

        public async Task<string> SendWhatsAppAsync(string toNumber, string message)
        {
            TwilioClient.Init(_accountSid, _authToken);

            var messageResult = await MessageResource.CreateAsync(
                to: new PhoneNumber(NormalizeWhatsAppAddress(toNumber)),
                from: new PhoneNumber(_whatsAppFromNumber),
                body: message
            );

            return messageResult.Sid;
        }

        private static string GetRequiredConfigValue(IConfiguration config, string key)
        {
            var value = config[key];

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException($"Missing required configuration value: {key}");
            }

            return value.Trim();
        }

        private static string NormalizeWhatsAppAddress(string phoneNumber)
        {
            var trimmedNumber = phoneNumber.Trim();

            return trimmedNumber.StartsWith("whatsapp:", StringComparison.OrdinalIgnoreCase)
                ? trimmedNumber
                : $"whatsapp:{trimmedNumber}";
        }
    }
}
