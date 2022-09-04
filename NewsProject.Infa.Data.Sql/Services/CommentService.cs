using Microsoft.EntityFrameworkCore;
using NewsProject.Core.Domain.Dtos.Comments;
using NewsProject.Core.Domain.Entities;
using NewsProject.Core.Domain.Entities.RequestParameters;
using NewsProject.Core.Domain.Services;
using NewsProject.Framework.Extensions;
using NewsProject.Infa.Data.Sql.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsProject.Infa.Data.Sql.Services
{
    public class CommentService : ICommentService
    {
        private readonly NewsDbContext _context;

        public CommentService(NewsDbContext context)
        {
            _context = context;
        }

        public void CreateComment(Guid newsId, CommentCreationDto comment)
        {
            var news = _context.Newses.Find(newsId);
            if (news == null)
                throw new Exception("News Not Found...!");
            Comment result = new Comment
            {
                News = news,
                CommentId = Guid.NewGuid(),
                CommentBy = comment.CommentBy,
                CommentText = comment.CommentText,
                CommentStatus = CommentStatus.UnApproved,
                CommentTime = DateTime.Now.ToShamsiDateTime(),
            };
            if (string.IsNullOrEmpty(result.CommentBy))
            {
                result.CommentBy = "بدون نام";
            }
            _context.Comments.Add(result);
            _context.SaveChanges();
        }

        public void AcceptComment(Guid id)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.CommentId == id);
            comment.CommentStatus = CommentStatus.Approved;
            _context.Comments.Update(comment);
            _context.SaveChanges();
        }

        public void DeleteComment(Guid id)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.CommentId == id);
            comment.CommentStatus = CommentStatus.Deleted;
            _context.Comments.Update(comment);
            _context.SaveChanges();
        }

        public Comment GetComment(Guid id)
        {
            var comment = _context.Comments.AsNoTracking().SingleOrDefault(c=>c.CommentId.Equals(id));
            return comment;
        }

        public PagedList<Comment> GetComments(CommentsParameters commentsParameters)
        {
            var comments = _context.Comments.AsNoTracking().Where(c=>c.CommentStatus == CommentStatus.UnApproved).ToList();
            return PagedList<Comment>.ToPagedList(comments, commentsParameters.PageNumber, commentsParameters.PageSize);
        }

        public void UpdateComment(Guid id, CommentUpdateDto comment)
        {
            var findComment = _context.Comments.SingleOrDefault(c => c.CommentId.Equals(id));
            findComment.CommentStatus = CommentStatus.Edited;
            findComment.CommentText = comment.CommentText;
            _context.Update(findComment);
            _context.SaveChanges();

        }

        public Comment GetCommentforUpdate(Guid id)
        {
            var comment = _context.Comments.AsNoTracking().Select(c => new Comment
            {
                CommentId = c.CommentId,
                CommentText = c.CommentText
            }).SingleOrDefault(c => c.CommentId.Equals(id));
            return comment;
        }
    }
}
