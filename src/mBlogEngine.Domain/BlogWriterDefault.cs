namespace mBlogEngine.Domain
{
	public class BlogWriterDefault : IBlogWriter
	{
		public string Decorated(Post post)
		{
			return string.Format("<article><h1>{0}</h1><p>{1}</p></article>", post.Title, post.Text);
		}
	}
}