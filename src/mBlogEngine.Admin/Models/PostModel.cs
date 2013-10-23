using System.ComponentModel.DataAnnotations;

namespace mBlogEngine.Admin.Models
{
	public class PostModel
	{
		[Required(ErrorMessage = "The post's title is necessary")]
		public string Title { get; set; }

		[Required(ErrorMessage = "The post's text is necessary")]
		public string Text { get; set; }
	}
}