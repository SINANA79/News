using System.ComponentModel.DataAnnotations;

namespace NewsProject.Endpoint.UI.Dtos.Comment
{
    public class CommentUpdateDto
    {
        [Required(ErrorMessage = "لطفا متن نظر را وارد کنید")]
        [MaxLength(350, ErrorMessage = "متن نظر نمیتواند بیشتر از {0} کاراکتر باشد")]
        public string CommentText { get; set; }
    }
}
