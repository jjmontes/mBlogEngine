using System.Collections.Generic;
using System.Linq;

namespace mBlogEngine.Domain
{
	public class Blog
	{
		private readonly IBlogNotifier _blogNotifier;
		private readonly IList<Post> _posts;

		public Blog(IBlogWriter blogWriter = null)
		{
			Writer = blogWriter ?? new BlogWriterDefault();
			_posts = new List<Post>();
		}

		public Blog(IBlogNotifier blogNotifier)
		{
			_blogNotifier = blogNotifier;
			_posts = new List<Post>();
		}

		public IBlogWriter Writer { get; private set; }

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

		public IBlogNotifier Notifier()
		{
			return _blogNotifier;
		}
	}
}
