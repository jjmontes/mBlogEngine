using System.Collections.Generic;
using System.Linq;

namespace mBlogEngine.Domain
{
	public class Blog
	{
		private readonly List<IBlogNotifier> _blogNotifiers;
		private readonly IList<Post> _posts;

		public Blog()
		{
			Writer = new BlogWriterDefault();
			_blogNotifiers = new List<IBlogNotifier>();
			_posts = new List<Post>();
			Title = string.Empty;
			Pages = new Pages(this);
		}

		public Blog(IEnumerable<IBlogNotifier> blogNotifiers)
			: this()
		{
			_blogNotifiers.AddRange(blogNotifiers);
		}

		public Blog(IBlogWriter blogWriter)
			: this()
		{
			Writer = blogWriter;
		}

		public Blog(IEnumerable<IBlogNotifier> blogNotifiers, IBlogWriter blogWriter)
			: this(blogNotifiers)
		{
			Writer = blogWriter;
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

		public IEnumerable<IBlogNotifier> Notifiers
		{
			get { return _blogNotifiers; }
		}

		public string Title { get; private set; }

		public Pages Pages { get; private set; }

		public Blog SetTitle(string title)
		{
			Title = title;
			return this;
		}
	}
}
