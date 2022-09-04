namespace NewsProject.Endpoint.UI.Dtos.Category
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
