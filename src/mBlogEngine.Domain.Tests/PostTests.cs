using System.Linq;
using NUnit.Framework;

namespace mBlogEngine.Domain.Tests
{
	[TestFixture]
    public class PostTests
    {
		[Test]
		public void CreateBasicPost()
		{
			var blog = new Blog();
			var post = blog.NewPost()
			               .SetTitle("Título")
			               .SetText("Primer post");

			Assert.AreEqual("Título", post.Title);
			Assert.AreEqual("Primer post", post.Text);
			Assert.AreEqual(null, post.Summary);
		}

		[Test]
		public void PublishPostAtBlog()
		{
			var blog = new Blog();
			Assert.AreEqual(0, blog.Posts.Count());

			blog.NewPost()
			    .SetTitle("Título")
			    .SetText("Primer post")
			    .Publish();

			Assert.AreEqual(1, blog.Posts.Count());
			Assert.AreEqual(1, blog.PublishedPosts.Count());
		}

		[Test]
		public void CreatePostUnpublishedAtBlog()
		{
			var blog = new Blog();
			Assert.AreEqual(0, blog.Posts.Count());

			blog.NewPost()
				.SetTitle("Título")
				.SetText("Primer post");

			Assert.AreEqual(1, blog.Posts.Count());
			Assert.AreEqual(0, blog.PublishedPosts.Count());
		}
	}
}
