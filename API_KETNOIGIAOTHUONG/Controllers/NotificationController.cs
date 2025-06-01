using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.Models;

namespace API_KETNOIGIAOTHUONG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly KNGTContext _context;

        public NotificationController(KNGTContext context)
        {
            _context = context;
        }

        // ✅ GET: api/Notification
        // ➤ Lấy tất cả thông báo cho tất cả người dùng (cẩn thận dữ liệu lớn)
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        //{
        //    return await _context.Notifications
        //        .Include(n => n.User)
        //        .OrderByDescending(n => n.CreatedDate)
        //        .ToListAsync();
        //}

        // ✅ GET: api/Notification/User/5
        // ➤ Lấy tất cả thông báo của 1 người dùng cụ thể (UserID)
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetUserNotifications(int userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserID == userId)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();

            if (notifications == null || notifications.Count == 0)
                return NotFound($"Không tìm thấy thông báo cho UserID = {userId}");

            return notifications;
        }

        // ✅ GET: api/Notification/5
        // ➤ Lấy thông báo theo NotificationID
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
                return NotFound();

            return notification;
        }

        // ✅ POST: api/Notification
        // ➤ Tạo thông báo mới cho người dùng
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
            notification.CreatedDate = DateTime.Now;
            notification.IsRead = false;

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotification), new { id = notification.NotificationID }, notification);
        }

        // ✅ PUT: api/Notification/5/Read
        // ➤ Đánh dấu thông báo đã đọc
        [HttpPut("{id}/Read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
                return NotFound();

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ DELETE: api/Notification/5
        // ➤ Xóa thông báo theo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);

            if (notification == null)
                return NotFound();

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
