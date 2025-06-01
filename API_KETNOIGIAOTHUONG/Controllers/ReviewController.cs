using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.Models;

namespace API_KETNOIGIAOTHUONG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly KNGTContext _context;

        public ReviewController(KNGTContext context)
        {
            _context = context;
        }

        // ✅ GET: api/Review
        // ➤ Lấy danh sách tất cả đánh giá
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return await _context.Reviews
                //.Include(r => r.SenderCompany)
                //.Include(r => r.ReceiverCompany)
                .Include(r => r.Contract)
                .ToListAsync();
        }

        // ✅ GET: api/Review/5
        // ➤ Lấy chi tiết đánh giá theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Reviews
               // .Include(r => r.SenderCompany)
               // .Include(r => r.ReceiverCompany)
                .Include(r => r.Contract)
                .FirstOrDefaultAsync(r => r.ReviewID == id);

            if (review == null)
                return NotFound();

            return review;
        }

        // ✅ POST: api/Review
        // ➤ Tạo mới đánh giá giao dịch/đối tác
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            // Kiểm tra giá trị rating hợp lệ (1 đến 5)
            if (review.Rating < 1 || review.Rating > 5)
            {
                return BadRequest("Rating phải nằm trong khoảng từ 1 đến 5.");
            }

            review.ReviewDate = DateTime.Now;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReview), new { id = review.ReviewID }, review);
        }

        // ✅ PUT: api/Review/5
        // ➤ Cập nhật đánh giá
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.ReviewID)
                return BadRequest();

            if (review.Rating < 1 || review.Rating > 5)
            {
                return BadRequest("Rating phải nằm trong khoảng từ 1 đến 5.");
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // ✅ DELETE: api/Review/5
        // ➤ Xóa đánh giá theo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(r => r.ReviewID == id);
        }
    }
}
