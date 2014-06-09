namespace mBlogEngine.Domain
{
	public abstract class Page
	{
		protected internal readonly Blog Blog;

		protected Page(Blog blog)
		{
			Blog = blog;
		}

		public abstract string Text { get; }
	}
}
