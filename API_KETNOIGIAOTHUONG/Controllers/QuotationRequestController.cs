using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.Models;

namespace API_KETNOIGIAOTHUONG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationRequestController : ControllerBase
    {
        private readonly KNGTContext _context;

        public QuotationRequestController(KNGTContext context)
        {
            _context = context;
        }

        // ✅ GET: api/QuotationRequest
        // ➤ Lấy danh sách tất cả yêu cầu báo giá
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuotationRequest>>> GetQuotationRequests()
        {
            return await _context.QuotationRequests.ToListAsync();
        }

        // ✅ GET: api/QuotationRequest/5
        // ➤ Lấy thông tin yêu cầu báo giá theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<QuotationRequest>> GetQuotationRequest(int id)
        {
            var quotationRequest = await _context.QuotationRequests.FindAsync(id);

            if (quotationRequest == null)
            {
                return NotFound();
            }

            return quotationRequest;
        }

        // ✅ POST: api/QuotationRequest
        // ➤ Tạo mới một yêu cầu báo giá
        [HttpPost]
        public async Task<ActionResult<QuotationRequest>> PostQuotationRequest(QuotationRequest quotationRequest)
        {
            quotationRequest.CreatedDate = DateTime.Now;
            _context.QuotationRequests.Add(quotationRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuotationRequest", new { id = quotationRequest.RequestID }, quotationRequest);
        }

        // ✅ PUT: api/QuotationRequest/5
        // ➤ Cập nhật thông tin yêu cầu báo giá theo ID
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuotationRequest(int id, QuotationRequest quotationRequest)
        {
            if (id != quotationRequest.RequestID)
            {
                return BadRequest();
            }

            _context.Entry(quotationRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuotationRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // ✅ DELETE: api/QuotationRequest/5
        // ➤ Xoá yêu cầu báo giá theo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotationRequest(int id)
        {
            var quotationRequest = await _context.QuotationRequests.FindAsync(id);
            if (quotationRequest == null)
            {
                return NotFound();
            }

            _context.QuotationRequests.Remove(quotationRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuotationRequestExists(int id)
        {
            return _context.QuotationRequests.Any(e => e.RequestID == id);
        }
    }
}
