using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.Models;
using XetTuyenVLU.ViewModels.Notification;

namespace XetTuyenVLU.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("GetAllNotifications")]
        public IActionResult GetAllNotifications()
        {
            var result = _notificationService.GetAllNotifications();
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpPost("CreateNotification")]
        public async Task<IActionResult> CreateNotification([FromForm] NotificationCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _notificationService.CreateNotification(request);
            if (result == 0)
                return BadRequest("Create notification was unsuccessfully!");
            return Ok(result);
        }

        [HttpGet("GetAllNotificationCategories")]
        public IActionResult GetAllNotificationCategories()
        {
            var result = _notificationService.GetAllNotificationCategories();
            if (result.Count == 0)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpGet("GetNotificationById/{id}")]
        public IActionResult GetNotificationById(int id)
        {
            var result = _notificationService.GetNotificationById(id);
            if (result == null)
                return NotFound("Not Found!");
            return Ok(result);
        }

        [HttpPut("ChangeStatusNotification/{id}")]
        public async Task<IActionResult> ChangeStatusNotification(int id)
        {
            var result = await _notificationService.ChangeStatusNotification(id);
            if (!result)
                return BadRequest("Change status notification was unsuccessfully!");
            return Ok(result);
        }
    }
}
