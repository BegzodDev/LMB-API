namespace LMB.Domain
{
    public class Link
    {
        public Guid Id { get; set; }
        public Guid LinkProfileId { get; set; }
        public string Title { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string? IconUrl { get; set; }
        public int OrderNo { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public LinkProfile? LinkProfile { get; set; }
    }
}
