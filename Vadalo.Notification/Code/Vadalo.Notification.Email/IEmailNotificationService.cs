using System.Threading.Tasks;

namespace Vadalo.Notification;

public interface IEmailNotificationService
{
    Task SendNotification(
        EmailNotificationRequest emailNotificationRequest
    );
}