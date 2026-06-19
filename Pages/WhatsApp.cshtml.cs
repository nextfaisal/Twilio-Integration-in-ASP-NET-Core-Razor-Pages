using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Twil.Pages
{
    public class WhatsAppModel : PageModel
    {
        private readonly TwilioSmsService _twilioSmsService;
        private readonly ILogger<WhatsAppModel> _logger;

        public WhatsAppModel(TwilioSmsService twilioSmsService, ILogger<WhatsAppModel> logger)
        {
            _twilioSmsService = twilioSmsService;
            _logger = logger;
        }

        [BindProperty]
        [Required]
        [Phone]
        [Display(Name = "Recipient WhatsApp number")]
        public string ToNumber { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        [StringLength(1600, MinimumLength = 1)]
        public string Message { get; set; } = string.Empty;

        [TempData]
        public string? StatusMessage { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var messageSid = await _twilioSmsService.SendWhatsAppAsync(ToNumber, Message);
                StatusMessage = $"WhatsApp message sent. Twilio SID: {messageSid}";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send WhatsApp message through Twilio.");
                ErrorMessage = "Unable to send the WhatsApp message. Check your Twilio credentials and WhatsApp sender settings.";
                return Page();
            }
        }
    }
}
