namespace LMB.Application.DTOs
{
    public class LinkProfileDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Theme { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
