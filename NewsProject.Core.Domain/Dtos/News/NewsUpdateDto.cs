using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsProject.Core.Domain.Dtos.News
{
    public class NewsUpdateDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public bool IsImportant { get; set; }
        public Guid CategoryId { get; set; }
    }
}
