using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DrawingApp.Models;
using System.Collections.Generic;
using System;

namespace DrawingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromptController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _geminiApiKey;

        private const string GeminiBaseUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";

        public PromptController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _geminiApiKey = _configuration["Gemini:ApiKey"]
                ?? throw new ArgumentNullException("Gemini:ApiKey not found in configuration.");
        }

        [HttpPost]
        public async Task<ActionResult<List<DrawingCommand>>> GenerateDrawing([FromBody] DrawingPromptRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return BadRequest("�� ����� ������.");

            var PrevDrawingsJson = request.PrevDrawings != null && request.PrevDrawings.Count > 0
            ? JsonSerializer.Serialize(request.PrevDrawings)
            : "[]";

            string drawingCommandExampleJson = "[{\"Shape\": \"rectangle\", \"X\": 10, \"Y\": 20, \"Width\": 50, \"Height\": 30, \"Color\": \"red\"}]";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = @$"����� ����� ���: {PrevDrawingsJson}.\n" +
" ���� �� ����� �����:" +
" ��� �� ����� ����� ��� ������ JSON �� �������� `DrawingCommand`. �������� `DrawingCommand` ������ ����� �������� ���� `Shape` (������, ���� ����� �� 'rectangle', 'circle', 'line' �� 'triangle'), `X` (���� ���, ����� �����), `Y` (���� ���, ����� ����), `Radius` (���� ���, ����� ���� �����), `Width` (���� ���, ���� ���� ���� ������), `Height` (���� ���, ���� ���� ���� ������), `Color` (������, �� �� ���), `From` (���� �� ��� ������ ����� [X, Y] ������ ����� �� ��), `To` (���� �� ��� ������ ����� [X, Y] ������ ���� �� ��). " +
" ���� ������: '{description}'. " +
" ���� �� �� ���� �-JSON, ��� ���� ����, ������ �� ������ �� ���� ��� Markdown. " +
" " +
"**������ ������� ������ ������ ��������� ����������, ��� ����� �������� �����:** " +
" " +
"1.  **����� ���� ���� ����� ������ �������:** " +
"    * **��� �� ������ �� ��� ��� ��� ����� (����) ����� ����� �������������.** �� ���� �� �� �������� ����� ��� �� ����� ����. " +
"    * ���� �� ����� ������. �� ����� ����, ����� ����� ��� ���� �� ����������� X �-Y. " +
"    * **����������� X ������ ����� ����� �� �-0 �� 480, ������������ Y ����� �� �-0 �� 380.** (���� ������ ��� ����� ����� ����� ���). " +
" " +
"2.  **����� ����, ���������� ����� ��� ����:** " +
"    * **��� �� ������ �������� ��������, ������ ���� ����� ���� �����.** ������� ��� '���' ������ ����� ���� ������ (Y ���� ����, ���� �-0), '���' �� '����' ���� ������ (Y ���� ����, ���� �-380). " +
"    * **�� ������� ����� ����� �� ����� (��� ���, ��, ���, ������) ���� ����� ����� �� ����� ������ ���� �� ���� ����� ���� '�����' (������, ����).** �� ����� ���������� ���� ������, ��� �� �� �� ����� (����, ��� �� �����). " +
" " +
"3.  **���������� �����:** " +
"    * **��� ����� ������ �������������� ������� ���� ���������� ����� ����� ����� ���� ����� ����.** ������, ��� ����� ����� ����� ����� �� �� ����� ����� �� ������������. ��� ���� ����� ����� ���� ���� ��� �� ����. " +
"    * **�� ����� ����� ����� ��� ���� ����� �� ������ ��� ������� �� �� ����.** " +
" " +
"4.  **����� ����� ������:** " +
"    * **���� ���� �� ������ �������� ������ ������.** �� ������ ���� ��� ������ (������, '������ �����'), ����� ���� �� ���� �������� ��������. " +
"    * �� �� ���� ���, ��� ����� �������� ���������� �������� �������� (������, ��� ����, ��� �����). " +
" " +
"5.  **��������� �� ������� ����� (����� �� ����� ����):** " +
"    * ���� ����� ������ ����� ��� ����� ����, ��� �� ������ ���� ����� ������ ���� �������� ���� �����. ������, �� �� ��� '��� ����', ��� ����� '������ ���', ���� ���� ����� �� ���� ����� ������ ���� �� ���� ���� ����. **�� ���� �� ����� �������� ������.** " +
" " +
"6.  **������ ������� ������:** " +
"    **����� ����� �-JSON ������ ���� ��������� ������ ��� ������ ���. ���� �� �������� (X, Y) ��� ���� ���� ����� ���� �� ����� ��� ���� ������, �� ���� �� ����� ������ ������������ �� ����� ��������.** " +
" " +
"    * **����� �'���':** " +
"        ```json " +
"        [ " +
"          {{\"Shape\": \"rectangle\", \"X\": 200, \"Y\": 250, \"Width\": 100, \"Height\": 80, \"Color\": \"brown\"}}, " +
"          {{\"Shape\": \"triangle\", \"X\": 235, \"Y\": 170, \"Width\": 100, \"Height\": 80, \"Color\": \"red\"}}, " +
"          {{\"Shape\": \"rectangle\", \"X\": 235, \"Y\": 290, \"Width\": 30, \"Height\": 40, \"Color\": \"darkgray\"}}, " +
"          {{\"Shape\": \"circle\", \"X\": 270, \"Y\": 270, \"Radius\": 15, \"Color\": \"lightblue\"}} " +
"        ] " +
"        ``` " +
" " +
"    * **����� �'���':** " +
"        ```json " +
"        [ " +
"          {{\"Shape\": \"circle\", \"X\": 250, \"Y\": 200, \"Radius\": 20, \"Color\": \"peachpuff\"}}, " +
"          {{\"Shape\": \"rectangle\", \"X\": 240, \"Y\": 220, \"Width\": 20, \"Height\": 60, \"Color\": \"blue\"}}, " +
"          {{\"Shape\": \"line\", \"From\": [240, 280], \"To\": [230, 320], \"Color\": \"black\"}}, " +
"          {{\"Shape\": \"line\", \"From\": [260, 280], \"To\": [270, 320], \"Color\": \"black\"}}, " +
"          {{\"Shape\": \"line\", \"From\": [240, 230], \"To\": [210, 250], \"Color\": \"black\"}}, " +
"          {{\"Shape\": \"line\", \"From\": [260, 230], \"To\": [290, 250], \"Color\": \"black\"}} " +
"        ] " +
"        ``` " +
" " +
"    * **����� �'��':** " +
"        ```json " +
"        [ " +
"          {{\"Shape\": \"rectangle\", \"X\": 100, \"Y\": 280, \"Width\": 30, \"Height\": 70, \"Color\": \"saddlebrown\"}}, " +
"          {{\"Shape\": \"circle\", \"X\": 115, \"Y\": 250, \"Radius\": 40, \"Color\": \"forestgreen\"}} " +
"        ] " +
"        ``` " +
" " +
"    * **����� �'������':** " +
"        ```json " +
"        [ " +
"          {{\"Shape\": \"rectangle\", \"X\": 100, \"Y\": 300, \"Width\": 120, \"Height\": 40, \"Color\": \"gray\"}}, " +
"          {{\"Shape\": \"circle\", \"X\": 125, \"Y\": 340, \"Radius\": 15, \"Color\": \"black\"}}, " +
"          {{\"Shape\": \"circle\", \"X\": 195, \"Y\": 340, \"Radius\": 15, \"Color\": \"black\"}} " +
"        ] " +
"        ``` " +
" " +
"    * **����� �'���':** " +
"        ```json " +
"        [ " +
"          {{\"Shape\": \"circle\", \"X\": 400, \"Y\": 80, \"Radius\": 30, \"Color\": \"gold\"}} " +
"        ] " +
"        ``` " +
" " +
"    * **����� �'���':** " +
"        ```json " +
"        [ " +
"          {{\"Shape\": \"rectangle\", \"X\": 0, \"Y\": 350, \"Width\": 500, \"Height\": 50, \"Color\": \"limegreen\"}} " +
"        ] " +
"        ``` " +
" " +
"    * **�� �������� ������ ���� ���� ��������� �������:** " +
"        **��� �� �������� ������ �������� ����� (rectangle, circle, line, triangle). ��� ���� ����� ������, �� ���������� ������, ��������� ������ ������� �� �������� ���� ������. ���� �� ����� ���� �� ��� ����� ������ ������ �������.** " +
" " +
$"����� ��� JSON �����: {drawingCommandExampleJson}"

                            }
                        }
                    }
                },
                generationConfig = new
                {
                    temperature = 0.7,
                    maxOutputTokens = 2048
                }
            };

            var jsonRequest = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
            var url = $"{GeminiBaseUrl}?key={_geminiApiKey}";

            try
            {
                var response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var geminiResponse = JsonSerializer.Deserialize<GeminiPromptResponse>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var text = geminiResponse?.Candidates?[0]?.Content?.Parts?[0]?.Text ?? "";

                // ����� ����� markdown �� ������
                text = text.Trim();
                if (text.StartsWith("```json"))
                    text = text.Substring(7).Trim();
                if (text.StartsWith("```"))
                    text = text.Substring(3).Trim();
                if (text.EndsWith("```"))
                    text = text[..^3].Trim();

                var commands = JsonSerializer.Deserialize<List<DrawingCommand>>(text, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return Ok(commands);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"����� ����� �-Gemini: {ex.Message}");
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"����� ������ JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"����� �����: {ex.Message}");
            }
        }
    }
}

