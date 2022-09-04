using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsProject.Core.Domain.Entities
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string CommentBy { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentTime { get; set; }
        public CommentStatus CommentStatus { get; set; }
        public News News { get; set; }
    }

    public enum CommentStatus
    {
        UnApproved,
        Approved,
        Edited,
        Deleted
    }
}
