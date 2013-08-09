using System.Collections.Generic;

namespace mBlogEngine.Domain
{
	public class Blog
	{
		private readonly IList<Post> _posts;

		public Blog()
		{
			_posts = new List<Post>();
		}

		public Post NewPost()
		{
			var post = new Post(this);
			_posts.Add(post);
			return post;
		}

		public IEnumerable<Post> Posts 
		{
			get { return _posts; }
		}
	}
}
