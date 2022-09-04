namespace NewsProject.Endpoint.UI.Dtos
{
    public class SliderDto
    {
        public Guid NewsId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryTitle { get; set; }
    }
}
