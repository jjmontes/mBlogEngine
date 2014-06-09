namespace mBlogEngine.Domain
{
	public interface IBlogWriter
	{
		string Decorated(Post post);
		string Decorated(Index page);
	}
}
