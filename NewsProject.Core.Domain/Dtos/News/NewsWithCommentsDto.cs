using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsProject.Core.Domain.Dtos.News
{
    public class NewsWithCommentsDto
    {
        public Guid NewsId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public CommentsForCountDto CommentsForCount { get; set; }
    }

    public class CommentsForCountDto
    {
        public Guid CommentId { get; set; }
    }
}
