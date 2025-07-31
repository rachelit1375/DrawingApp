using System;

public class Drawing
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public string Name { get; set; } = "";
	public string Prompt { get; set; } = "";

	//public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	//public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

	// public List<DrawingCommand> Commands { get; set; } = new();
}
