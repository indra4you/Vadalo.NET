using System.Collections.Generic;
using System.Reflection;

namespace Vadalo.Notification;

public sealed class EmailNotificationRequest(
    IEnumerable<string> sendTo,
    string subject,
    Assembly fromAssembly,
    string templateNameWithoutExtension,
    IDictionary<string, string>? keyValues = null,
    string templatePath = "Templates"
)
{
    public IEnumerable<string> SendTo = sendTo;

    public string Subject = subject;

    public Assembly FromAssembly = fromAssembly;

    public string TemplateNameWithoutExtension = templateNameWithoutExtension;

    public IDictionary<string, string>? KeyValues = keyValues;

    public string TemplatePath = templatePath;
}