using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DrawingApp.Models;

using System;

namespace DrawingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                return BadRequest("Username is required");

            if (string.IsNullOrWhiteSpace(user.Password))
                return BadRequest("Password is required");

            var existing = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (existing != null)
                return Conflict("Username already taken");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user); // מחזיר את המשתמש כולל Id
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == login.Username);

            if (user == null)
                return NotFound("User not found");

            if (user.Password != login.Password)
                return Unauthorized("Incorrect password");

            return Ok(user);
        }

    }
}
