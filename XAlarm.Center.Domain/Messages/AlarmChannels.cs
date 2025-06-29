using System.ComponentModel;

namespace XAlarm.Center.Domain.Messages;

public enum AlarmChannels
{
    [Description("line")] Line = 0,
    [Description("telegram")] Telegram = 1,
    [Description("email")] Email = 2,
    [Description("sms")] Sms = 3
}