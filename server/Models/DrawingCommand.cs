using System;

public class DrawingCommand
{
    public Guid Id { get; set; }
    public Guid DrawingId { get; set; }
    public string CommandJson { get; set; } = "";
    public int Order { get; set; }  // לצורך Undo/Redo
}
