using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using DrawingApp.Models;

using System;

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

        // ����� �� ������� �� �����
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDrawingsByUser(Guid userId)
        {
            var drawings = await _context.Drawings
                .Where(d => d.UserId == userId)
                .ToListAsync();

            return Ok(drawings);
        }

        // ����� ���� ������ ��� ����
        [HttpGet("{drawingId}")]
        public async Task<IActionResult> GetDrawing(Guid drawingId)
        {
            var drawing = await _context.Drawings
                //.Include(d => d.Id) // ���� �� ������ Include �� ������ �����
                .FirstOrDefaultAsync(d => d.Id == drawingId);

            if (drawing == null)
                return NotFound();

            return Ok(drawing);
        }

        // ����� ���� ���
        [HttpPost]
        public async Task<IActionResult> SaveDrawing([FromBody] Drawing drawing)
        {
            drawing.Id = Guid.NewGuid();  // ��� ���� ��������
            _context.Drawings.Add(drawing);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDrawing), new { drawingId = drawing.Id }, drawing);
        }

        // ����� ����
        [HttpDelete("{drawingId}")]
        public async Task<IActionResult> DeleteDrawing(Guid drawingId)
        {
            var drawing = await _context.Drawings.FindAsync(drawingId);
            if (drawing == null)
                return NotFound();

            _context.Drawings.Remove(drawing);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
