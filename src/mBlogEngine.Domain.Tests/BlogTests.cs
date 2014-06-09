using NUnit.Framework;

namespace mBlogEngine.Domain.Tests
{
	[TestFixture]
	public class BlogTests
	{
		[Test]
		public void TitleWhenIsNewBlog()
		{
			var blog = new Blog();
			Assert.AreEqual("", blog.Title);
		}

		[Test]
		public void IndexPageWhenIsNewBlog()
		{
			var blog = new Blog();
			var index = blog.Pages.Get("Index");
			StringAssert.Contains("<title></title>", index.Text);
			StringAssert.Contains("<body>", index.Text);
			StringAssert.Contains("</body>", index.Text);
			StringAssert.Contains("<div class=\"container\">", index.Text);
		}
	}
}
