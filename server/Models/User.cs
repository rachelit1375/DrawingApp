using System;
using System.Collections.Generic;

namespace DrawingApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = "";
        public string? PasswordHash { get; set; }
        public List<Drawing> Drawings { get; set; } = new();
    }
}
