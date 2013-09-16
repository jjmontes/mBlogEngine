using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;

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

		[Test]
		public void PostDecoratedByDefaultBlog()
		{
			var blog = new Blog();

			var post = blog.NewPost()
			               .SetTitle("Título")
			               .SetText("Primer post");

			Assert.AreEqual("<article><h1>Título</h1><p>Primer post</p></article>", post.Decorated);
		}

		[Test]
		public void PostDecoratedBySpecifyBlog()
		{
			var moqBlogWriter = new Mock<IBlogWriter>();
			moqBlogWriter.Setup(w => w.Decorated(It.IsAny<Post>()))
			             .Returns("<div class='title'>Título</div><div class='post'>Primer post</div>");

			var blog = new Blog(moqBlogWriter.Object);

			var post = blog.NewPost()
						   .SetTitle("Título")
						   .SetText("Primer post");

			Assert.AreEqual("<div class='title'>Título</div><div class='post'>Primer post</div>", post.Decorated);
		}

		[Test]
		public void NotifyWhenPostIsPublished()
		{
			var moqNotifier = new Mock<IBlogNotifier>();
			moqNotifier.Setup(n => n.PostIsPublished(It.IsAny<Post>()));

			var blog = new Blog(new[] {moqNotifier.Object});
			Assert.AreEqual(0, blog.Posts.Count());

			blog.NewPost()
				.SetTitle("Título")
				.SetText("Primer post")
				.Publish();

			moqNotifier.Verify(n => n.PostIsPublished(It.IsAny<Post>()), Times.Once());
		}

		[Test]
		public void UseTwoBlogNotifier()
		{
			var moqNotifierOne = new Mock<IBlogNotifier>();
			moqNotifierOne.Setup(n => n.PostIsPublished(It.IsAny<Post>()));
			var moqNotifierTwo = new Mock<IBlogNotifier>();
			moqNotifierTwo.Setup(n => n.PostIsPublished(It.IsAny<Post>()));

			var blog = new Blog(new [] { moqNotifierOne.Object, moqNotifierTwo.Object});
			Assert.AreEqual(0, blog.Posts.Count());

			blog.NewPost()
				.SetTitle("Título")
				.SetText("Primer post")
				.Publish();

			moqNotifierOne.Verify(n => n.PostIsPublished(It.IsAny<Post>()), Times.Once());
			moqNotifierTwo.Verify(n => n.PostIsPublished(It.IsAny<Post>()), Times.Once());
		}
	}
}
