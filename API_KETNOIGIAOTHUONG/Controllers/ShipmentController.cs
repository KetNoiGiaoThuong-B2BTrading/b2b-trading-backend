using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_KETNOIGIAOTHUONG.Data;
using API_KETNOIGIAOTHUONG.Models;

namespace API_KETNOIGIAOTHUONG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly KNGTContext _context;

        public ShipmentController(KNGTContext context)
        {
            _context = context;
        }

        // ✅ GET: api/Shipment
        // ➤ Lấy danh sách toàn bộ các giao hàng
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shipment>>> GetShipments()
        {
            return await _context.Shipments
                .Include(s => s.Contract)
                .ToListAsync();
        }

        // ✅ GET: api/Shipment/5
        // ➤ Lấy chi tiết giao hàng theo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Shipment>> GetShipment(int id)
        {
            var shipment = await _context.Shipments
                .Include(s => s.Contract)
                .FirstOrDefaultAsync(s => s.ShipmentID == id);

            if (shipment == null)
                return NotFound();

            return shipment;
        }

        // ✅ POST: api/Shipment
        // ➤ Tạo mới thông tin giao hàng
        [HttpPost]
        public async Task<ActionResult<Shipment>> PostShipment(Shipment shipment)
        {
            shipment.UpdateDate = DateTime.Now;

            _context.Shipments.Add(shipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShipment), new { id = shipment.ShipmentID }, shipment);
        }

        // ✅ PUT: api/Shipment/5
        // ➤ Cập nhật thông tin giao hàng
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipment(int id, Shipment shipment)
        {
            if (id != shipment.ShipmentID)
                return BadRequest();

            _context.Entry(shipment).State = EntityState.Modified;
            shipment.UpdateDate = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipmentExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // ✅ DELETE: api/Shipment/5
        // ➤ Xoá thông tin giao hàng
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipment(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
                return NotFound();

            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShipmentExists(int id)
        {
            return _context.Shipments.Any(s => s.ShipmentID == id);
        }
    }
}
