namespace NewsProject.Endpoint.UI.Dtos
{
    public class NewsWithCommentDto
    {
        public Guid NewsId { get; set; }
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string? Image { get; set; }
        public string Author { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool IsImportant { get; set; }
        public int View { get; set; }
        public List<CommentDto>? Comments { get; set; }
    }
}
