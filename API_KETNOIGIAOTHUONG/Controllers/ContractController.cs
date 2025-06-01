using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.Models;

namespace API_KETNOIGIAOTHUONG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly KNGTContext _context;

        public ContractController(KNGTContext context)
        {
            _context = context;
        }

        // ✅ GET: api/Contract
        // ➤ Lấy danh sách toàn bộ hợp đồng
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contract>>> GetContracts()
        {
            return await _context.Contracts.ToListAsync();
        }

        // ✅ GET: api/Contract/5
        // ➤ Lấy hợp đồng theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Contract>> GetContract(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
                return NotFound();

            return contract;
        }

        // ✅ POST: api/Contract
        // ➤ Tạo hợp đồng mới
        //[HttpPost]
        //public async Task<ActionResult<Contract>> PostContract(Contract contract)
        //{
        //    contract.Status = "PendingSign";
        //    contract.SignDate = null;
        //    _context.Contracts.Add(contract);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetContract), new { id = contract.ContractID }, contract);
        //}

        // ✅ PUT: api/Contract/Sign/5
        // ➤ Ký hợp đồng (cập nhật chữ ký & ngày ký)
        [HttpPut("Sign/{id}")]
        public async Task<IActionResult> SignContract(int id, [FromBody] SignContractModel model)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
                return NotFound();

            // Giả định ký cả hai bên
            contract.SellerSignature = model.SellerSignature;
            contract.BuyerSignature = model.BuyerSignature;
            contract.SignDate = DateTime.Now;
            contract.Status = "Signed";

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ✅ PUT: api/Contract/5
        // ➤ Cập nhật nội dung hợp đồng
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContract(int id, Contract contract)
        {
            if (id != contract.ContractID)
                return BadRequest();

            _context.Entry(contract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // ✅ DELETE: api/Contract/5
        // ➤ Xoá hợp đồng
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
                return NotFound();

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.ContractID == id);
        }
    }

    // ✅ Model dùng để ký hợp đồng
    public class SignContractModel
    {
        public string SellerSignature { get; set; } = string.Empty;
        public string BuyerSignature { get; set; } = string.Empty;
    }
}
