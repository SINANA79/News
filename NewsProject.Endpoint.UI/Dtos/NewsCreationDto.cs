namespace NewsProject.Endpoint.UI.Dtos
{
    public class NewsCreationDto
    {
        public Guid NewsId { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public bool IsImportant { get; set; }
        public string Image { get; set; }
    }
}
