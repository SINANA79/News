namespace NewsProject.Endpoint.UI.Dtos
{
    public class NewsCardDto
    {
        public Guid NewsId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public int View { get; set; }
        public DateTime CreateTime { get; set; }
        public string CategoryTitle { get; set; }

    }
}
