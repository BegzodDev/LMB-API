namespace LMB.Application.DTOs
{
    public class LinkDto
    {
        public Guid Id { get; set; }
        public Guid LinkProfileId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public int OrderNo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
