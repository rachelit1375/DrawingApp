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

        // שליפת כל הציורים של משתמש
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDrawingsByUser(Guid userId)
        {
            var drawings = await _context.Drawings
                .Where(d => d.UserId == userId)
                .ToListAsync();

            return Ok(drawings);
        }

        // שליפת ציור ספציפי לפי מזהה
        [HttpGet("{drawingId}")]
        public async Task<IActionResult> GetDrawing(Guid drawingId)
        {
            var drawing = await _context.Drawings
                //.Include(d => d.Id) // נוכל גם להוסיף Include של פקודות בעתיד
                .FirstOrDefaultAsync(d => d.Id == drawingId);

            if (drawing == null)
                return NotFound();

            return Ok(drawing);
        }

        // שמירת ציור חדש
        [HttpPost]
        public async Task<IActionResult> SaveDrawing([FromBody] Drawing drawing)
        {
            drawing.Id = Guid.NewGuid();  // ודא שאין כפילויות
            _context.Drawings.Add(drawing);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDrawing), new { drawingId = drawing.Id }, drawing);
        }

        // מחיקת ציור
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
