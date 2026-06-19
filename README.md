# Twilio Integration in ASP.NET Core Razor Pages

This project demonstrates how to integrate Twilio messaging into an ASP.NET Core Razor Pages application. It supports sending SMS messages and WhatsApp messages through Twilio.

## Features

- Send SMS using the Twilio API
- Send WhatsApp messages using the Twilio API
- Razor Pages UI for WhatsApp messaging
- Secure configuration using `appsettings.json`
- Clean and minimal .NET dependency injection setup

## Technologies Used

- ASP.NET Core 8.0 Razor Pages
- Twilio C# SDK
- .NET dependency injection
- Visual Studio / VS Code

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/nextfaisal/Twilio-Integration-in-ASP-NET-Core-Razor-Pages.git
cd Twilio-Integration-in-ASP-NET-Core-Razor-Pages
```

### 2. Configure Twilio

Update the `Twilio` section in `appsettings.json`:

```json
{
  "Twilio": {
    "AccountSID": "Your_TWILIO_SID_HERE",
    "AuthToken": "Your_TWILIO_AUTH_TOKEN_HERE",
    "FromPhoneNumber": "+15551234567",
    "WhatsAppFromNumber": "whatsapp:+14155238886"
  }
}
```

Use your Twilio SMS sender for `FromPhoneNumber`. For WhatsApp testing, `whatsapp:+14155238886` is Twilio's sandbox sender. Recipients must join your Twilio WhatsApp sandbox before test messages can be delivered.

### 3. Run the App

```bash
dotnet restore
dotnet run
```

Open the app in your browser and go to `/WhatsApp` to send a WhatsApp message.
