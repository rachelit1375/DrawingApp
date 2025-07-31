using Microsoft.AspNetCore.Mvc;
using System;

namespace YourProjectNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromptController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostPrompt([FromBody] PromptRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return BadRequest(new { message = "Prompt is required." });

            // תשובה מזויפת (stub)
            var fakeResponse = new
            {
                prompt = request.Prompt,
                response = $"זו תשובה מזויפת לפרומפט: '{request.Prompt}'",
                timestamp = DateTime.UtcNow
            };

            return Ok(fakeResponse);
        }
    }

    public class PromptRequest
    {
        public string Prompt { get; set; } = string.Empty;
    }
}
