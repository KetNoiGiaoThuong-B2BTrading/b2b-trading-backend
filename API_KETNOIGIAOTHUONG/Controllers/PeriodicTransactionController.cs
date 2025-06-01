using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.Models;

namespace API_KETNOIGIAOTHUONG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodicTransactionController : ControllerBase
    {
        private readonly KNGTContext _context;

        public PeriodicTransactionController(KNGTContext context)
        {
            _context = context;
        }

        // ✅ GET: api/PeriodicTransaction
        // ➤ Lấy tất cả các giao dịch định kỳ
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeriodicTransaction>>> GetPeriodicTransactions()
        {
            return await _context.PeriodicTransactions
                .OrderBy(pt => pt.DueDate)
                .ToListAsync();
        }

        // ✅ GET: api/PeriodicTransaction/5
        // ➤ Lấy giao dịch định kỳ theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PeriodicTransaction>> GetPeriodicTransaction(int id)
        {
            var transaction = await _context.PeriodicTransactions.FindAsync(id);

            if (transaction == null)
                return NotFound();

            return transaction;
        }

        // ✅ POST: api/PeriodicTransaction
        // ➤ Tạo giao dịch định kỳ mới
        [HttpPost]
        public async Task<ActionResult<PeriodicTransaction>> PostPeriodicTransaction(PeriodicTransaction periodicTransaction)
        {
            _context.PeriodicTransactions.Add(periodicTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPeriodicTransaction), new { id = periodicTransaction.TransactionID }, periodicTransaction);
        }

        // ✅ PUT: api/PeriodicTransaction/5
        // ➤ Cập nhật giao dịch định kỳ
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeriodicTransaction(int id, PeriodicTransaction periodicTransaction)
        {
            if (id != periodicTransaction.TransactionID)
                return BadRequest("ID không khớp.");

            _context.Entry(periodicTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodicTransactionExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // ✅ DELETE: api/PeriodicTransaction/5
        // ➤ Xóa giao dịch định kỳ
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeriodicTransaction(int id)
        {
            var transaction = await _context.PeriodicTransactions.FindAsync(id);
            if (transaction == null)
                return NotFound();

            _context.PeriodicTransactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeriodicTransactionExists(int id)
        {
            return _context.PeriodicTransactions.Any(e => e.TransactionID == id);
        }
    }
}
