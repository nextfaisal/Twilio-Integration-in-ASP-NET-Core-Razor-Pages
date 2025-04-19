using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Twil.Pages
{
    public class SendSMSModel : PageModel
    {
        private readonly TwilioSmsService _smsService;

        public SendSMSModel(TwilioSmsService smsService)
        {
            _smsService = smsService;
        }

        public async Task<IActionResult> OnPostSendSmsAsync()
        {
            string toPhoneNumber = "+923312017409"; // Replace with actual number
            string message = "Dear Farmer, your Abiana bill has been generated. Please check your account.";

            await _smsService.SendSmsAsync(toPhoneNumber, message);

            TempData["Message"] = "SMS sent successfully!";
            return RedirectToPage();
        }
    }
}
