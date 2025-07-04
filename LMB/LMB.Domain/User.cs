﻿using System.Security.Claims;

namespace LMB.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public LinkProfile? Profile { get; set; }
        //public ClaimsIdentity? Username { get; set; }
    }
}
