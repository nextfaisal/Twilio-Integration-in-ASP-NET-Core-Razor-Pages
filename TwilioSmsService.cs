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

        public TwilioSmsService(IConfiguration config)
        {
            _accountSid = config["Twilio:AccountSID"];
            _authToken = config["Twilio:AuthToken"];
            _fromNumber = config["Twilio:FromPhoneNumber"];
        }

        public async Task SendSmsAsync(string toNumber, string message)
        {
            TwilioClient.Init(_accountSid, _authToken);

            var messageResult = await MessageResource.CreateAsync(
                to: new PhoneNumber(toNumber),
                from: new PhoneNumber(_fromNumber),
                body: message
            );

            Console.WriteLine($"SMS Sent: SID = {messageResult.Sid}");
        }
    }

}
