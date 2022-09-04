using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsProject.Endpoint.UI.Dtos
{
    public class NewsWithCategoryDto
    {
        public Guid NewsId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public DateTime CreateTime { get; set; }
        public string Image { get; set; }
        public Guid CategoryId { get; set; }
        public int View { get; set; }
        public bool IsDeleted { get; set; }
        public string CategoryTitle { get; set; }
    }
}
