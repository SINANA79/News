using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsProject.Core.Domain.Dtos.Comments
{
    public class CommentCreationDto
    {
        public Guid CommentId { get; set; }
        public string CommentBy { get; set; }
        public string CommentText { get; set; }
    }
}
