using NUnit.Framework;

namespace mBlogEngine.Domain.Tests
{
	[TestFixture]
    public class PostTests
    {
		[Test]
		public void CrearUnPostBasico()
		{
			var post = new Post("Título") {Text = "Primer post"};

			Assert.AreEqual("Título", post.Title);
			Assert.AreEqual("Primer post", post.Text);
			Assert.AreEqual(null, post.Summary);
		}
    }
}
