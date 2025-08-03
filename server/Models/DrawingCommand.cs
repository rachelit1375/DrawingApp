using System;

namespace DrawingApp.Models
{
    public class DrawingCommand
    {
        public int Id { get; set; }
        public int DrawingId { get; set; }
        public string Shape { get; set; } = "";
        public int? X { get; set; }
        public int? Y { get; set; }
        public int? Radius { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string? Color { get; set; }
        public int[]? From { get; set; }
        public int[]? To { get; set; }
    }
}
