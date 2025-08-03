using System;

namespace DrawingApp.Models
{
    public class Drawing
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = "";
        public string Prompt { get; set; } = "";
        public string CommandJson { get; set; } = "";

        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // public List<DrawingCommand> Commands { get; set; } = new();
    }
}
