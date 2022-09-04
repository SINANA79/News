using NewsProject.Core.Domain.Dtos.Comments;
using NewsProject.Core.Domain.Entities;
using NewsProject.Core.Domain.Entities.RequestParameters;

namespace NewsProject.Core.Domain.Services
{
    public interface ICommentService
    {
        PagedList<Comment> GetComments(CommentsParameters commentsParameters);
        Comment GetComment(Guid id);
        Comment GetCommentforUpdate(Guid id);
        void CreateComment(Guid newsId, CommentCreationDto comment);
        void UpdateComment(Guid id, CommentUpdateDto comment);
        void AcceptComment(Guid id);
        void DeleteComment(Guid id);
    }
}
