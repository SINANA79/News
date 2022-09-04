namespace NewsProject.Endpoint.UI.Dtos
{
    public class NewsWithDependentsDto
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
        public List<TagDto>? Tags { get; set; }
        public CategoryForNewsDto Category { get; set; }
    }

    public class CommentDto
    {
        public Guid CommentsId { get; set; }
        public string CommentBy { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentTime { get; set; }
    }
    public class TagDto 
    {
        public Guid TagId { get; set; }
        public string TagName { get; set; }
    }

    public class CategoryForNewsDto
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
