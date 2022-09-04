namespace NewsProject.Endpoint.UI.Dtos.Comment
{
    public class GetCommentDto
    {
        public Guid CommentId { get; set; }
        public string CommentBy { get; set; }
        public string CommentText { get; set; }
    }
}
