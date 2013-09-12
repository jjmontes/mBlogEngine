namespace mBlogEngine.Domain
{
	public interface IBlogNotifier
	{
		void PostIsPublished(Post post);
	}
}
