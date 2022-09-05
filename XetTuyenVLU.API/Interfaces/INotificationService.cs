using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Notification;

namespace XetTuyenVLU.Interfaces
{
    public interface INotificationService
    {
        public NotificationVM GetNotificationById(int id);
        public List<NotificationVM> GetAllNotifications();
        public List<LoaiThongBao> GetAllNotificationCategories();
        public Task<int> CreateNotification(NotificationCreateRequest request);
        public Task<bool> ChangeStatusNotification(int id);
    }
}
