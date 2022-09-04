using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsProject.Endpoint.UI.Dtos.Comment
{
    public class CommentCreationDto
    {
        public Guid CommentId { get; set; }
        public string CommentBy { get; set; }

        [Required(ErrorMessage = "لطفا متن نظر را وارد کنید")]
        [MaxLength(350, ErrorMessage = "متن نظر نمیتواند بیشتر از {0} کاراکتر باشد")]
        public string CommentText { get; set; }
    }

}
