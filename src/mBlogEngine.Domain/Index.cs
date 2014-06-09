namespace mBlogEngine.Domain
{
	public class Index : Page
	{
		public Index(Blog blog)
			: base(blog)
		{
		}

		public override string Text
		{
			get { return Blog.Writer.Decorated(this); }
		}
	}
}