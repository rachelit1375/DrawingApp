using System.Collections.Generic;

namespace DrawingApp.Models
{
	public class DrawingPromptRequest
	{
		public string Prompt { get; set; } = "";
		public List<DrawingCommand>? PrevDrawings { get; set; }
	}
}
