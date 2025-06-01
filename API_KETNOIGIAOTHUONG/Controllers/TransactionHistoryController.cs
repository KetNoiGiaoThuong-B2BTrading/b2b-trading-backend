using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.Models;

namespace API_KETNOIGIAOTHUONG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly KNGTContext _context;

        public TransactionHistoryController(KNGTContext context)
        {
            _context = context;
        }

        // ✅ GET: api/TransactionHistory
        // ➤ Lấy tất cả bản ghi lịch sử giao dịch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionHistory>>> GetTransactionHistories()
        {
            return await _context.TransactionHistories
                .Include(th => th.Contract)
                .Include(th => th.PerformedByUser)
                .OrderByDescending(th => th.ActionTime)
                .ToListAsync();
        }

        // ✅ GET: api/TransactionHistory/5
        // ➤ Lấy lịch sử giao dịch theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionHistory>> GetTransactionHistory(int id)
        {
            var history = await _context.TransactionHistories
                .Include(th => th.Contract)
                .Include(th => th.PerformedByUser)
                .FirstOrDefaultAsync(th => th.HistoryID == id);

            if (history == null)
                return NotFound();

            return history;
        }

        // ✅ POST: api/TransactionHistory
        // ➤ Thêm bản ghi lịch sử giao dịch mới
        [HttpPost]
        public async Task<ActionResult<TransactionHistory>> PostTransactionHistory(TransactionHistory transactionHistory)
        {
            _context.TransactionHistories.Add(transactionHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransactionHistory), new { id = transactionHistory.HistoryID }, transactionHistory);
        }

        // ✅ PUT: api/TransactionHistory/5
        // ➤ Cập nhật bản ghi lịch sử giao dịch
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactionHistory(int id, TransactionHistory transactionHistory)
        {
            if (id != transactionHistory.HistoryID)
                return BadRequest("ID không khớp.");

            _context.Entry(transactionHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionHistoryExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // ✅ DELETE: api/TransactionHistory/5
        // ➤ Xóa bản ghi lịch sử giao dịch
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionHistory(int id)
        {
            var history = await _context.TransactionHistories.FindAsync(id);
            if (history == null)
                return NotFound();

            _context.TransactionHistories.Remove(history);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionHistoryExists(int id)
        {
            return _context.TransactionHistories.Any(e => e.HistoryID == id);
        }
    }
}
