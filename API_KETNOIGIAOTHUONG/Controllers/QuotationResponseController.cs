using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.Models;

namespace API_KETNOIGIAOTHUONG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationResponseController : ControllerBase
    {
        private readonly KNGTContext _context;

        public QuotationResponseController(KNGTContext context)
        {
            _context = context;
        }

        // ✅ GET: api/QuotationResponse
        // ➤ Lấy danh sách tất cả phản hồi báo giá
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuotationResponse>>> GetQuotationResponses()
        {
            return await _context.QuotationResponses.ToListAsync();
        }

        // ✅ GET: api/QuotationResponse/5
        // ➤ Lấy phản hồi báo giá theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<QuotationResponse>> GetQuotationResponse(int id)
        {
            var response = await _context.QuotationResponses.FindAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return response;
        }

        // ✅ POST: api/QuotationResponse
        // ➤ Tạo mới một phản hồi báo giá
        [HttpPost]
        public async Task<ActionResult<QuotationResponse>> PostQuotationResponse(QuotationResponse response)
        {
            response.CreatedDate = DateTime.Now;
            _context.QuotationResponses.Add(response);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuotationResponse", new { id = response.ResponseID }, response);
        }

        // ✅ PUT: api/QuotationResponse/5
        // ➤ Cập nhật phản hồi báo giá theo ID
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuotationResponse(int id, QuotationResponse response)
        {
            if (id != response.ResponseID)
                return BadRequest();

            _context.Entry(response).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuotationResponseExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // ✅ DELETE: api/QuotationResponse/5
        // ➤ Xoá phản hồi báo giá theo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotationResponse(int id)
        {
            var response = await _context.QuotationResponses.FindAsync(id);
            if (response == null)
                return NotFound();

            _context.QuotationResponses.Remove(response);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuotationResponseExists(int id)
        {
            return _context.QuotationResponses.Any(e => e.ResponseID == id);
        }
    }
}
