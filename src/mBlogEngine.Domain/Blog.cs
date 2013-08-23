using System.Collections.Generic;
using System.Linq;

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

		public IEnumerable<Post> PublishedPosts
		{
			get { return _posts.Where(p => p.Published); }
		}
	}
}
