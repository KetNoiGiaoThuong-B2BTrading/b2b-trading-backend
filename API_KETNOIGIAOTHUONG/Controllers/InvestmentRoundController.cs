using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.Models;

namespace API_KETNOIGIAOTHUONG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentRoundController : ControllerBase
    {
        private readonly KNGTContext _context;

        public InvestmentRoundController(KNGTContext context)
        {
            _context = context;
        }

        // ✅ GET: api/InvestmentRound
        // ➤ Lấy tất cả vòng gọi vốn
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvestmentRound>>> GetInvestmentRounds()
        {
            return await _context.InvestmentRounds
                .OrderByDescending(r => r.PlannedStartDate)
                .ToListAsync();
        }

        // ✅ GET: api/InvestmentRound/5
        // ➤ Lấy vòng gọi vốn theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<InvestmentRound>> GetInvestmentRound(int id)
        {
            var round = await _context.InvestmentRounds.FindAsync(id);

            if (round == null)
                return NotFound();

            return round;
        }

        // ✅ POST: api/InvestmentRound
        // ➤ Tạo vòng gọi vốn mới
        [HttpPost]
        public async Task<ActionResult<InvestmentRound>> PostInvestmentRound(InvestmentRound investmentRound)
        {
            _context.InvestmentRounds.Add(investmentRound);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvestmentRound), new { id = investmentRound.RoundID }, investmentRound);
        }

        // ✅ PUT: api/InvestmentRound/5
        // ➤ Cập nhật thông tin vòng gọi vốn
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestmentRound(int id, InvestmentRound investmentRound)
        {
            if (id != investmentRound.RoundID)
                return BadRequest("ID không khớp.");

            _context.Entry(investmentRound).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestmentRoundExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // ✅ DELETE: api/InvestmentRound/5
        // ➤ Xóa vòng gọi vốn
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestmentRound(int id)
        {
            var round = await _context.InvestmentRounds.FindAsync(id);
            if (round == null)
                return NotFound();

            _context.InvestmentRounds.Remove(round);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvestmentRoundExists(int id)
        {
            return _context.InvestmentRounds.Any(e => e.RoundID == id);
        }
    }
}
