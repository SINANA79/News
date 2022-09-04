namespace NewsProject.Endpoint.UI.Dtos
{
    public class NewsUpdateDto
    {
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public bool IsImportant { get; set; }
        public string Image { get; set; }
    }
}
