using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using DrawingApp.Models;

namespace DrawingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrawingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DrawingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/drawing/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDrawingsByUser(int userId)
        {
            var drawings = await _context.Drawings
                .Where(d => d.UserId == userId)
                .ToListAsync();

            return Ok(drawings);
        }

        // GET: api/drawing/3
        [HttpGet("{drawingId}")]
        public async Task<IActionResult> GetDrawing(int drawingId)
        {
            var drawing = await _context.Drawings
                .FirstOrDefaultAsync(d => d.Id == drawingId);

            if (drawing == null)
                return NotFound();

            return Ok(drawing);
        }

        // POST: api/drawing
        [HttpPost]
        public async Task<IActionResult> SaveDrawing([FromBody] Drawing drawing)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Drawings.Add(drawing);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDrawing), new { drawingId = drawing.Id }, drawing);
        }
    }
}
