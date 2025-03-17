using System.ComponentModel;

namespace XAlarm.Center.Domain.Events;

public enum EventTypes
{
    [Description("General")] General = 0,
    [Description("Information")] Information = 1,
    [Description("Error")] Error = 2,
    [Description("License not found")] LicenseNotFound = 3,
    [Description("Invalid license")] InvalidLicense = 4,
    [Description("Message received")] MessageReceived = 101,
    [Description("Email sent")] EmailSent = 102,
    [Description("Email error")] EmailError = 103,
    [Description("LINE Notify sent")] LineNotifySent = 104,
    [Description("LINE Notify error")] LineNotifyError = 105,
    [Description("LINE sent")] LineSent = 106,
    [Description("LINE error")] LineError = 107,
    [Description("Telegram sent")] TelegramSent = 108,
    [Description("Telegram error")] TelegramError = 109
}