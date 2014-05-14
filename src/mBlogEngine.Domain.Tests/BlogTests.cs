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
			StringAssert.Contains("<title></title>", blog.Index);
			StringAssert.Contains("<body>", blog.Index);
			StringAssert.Contains("</body>", blog.Index);
			StringAssert.Contains("<div class=\"container\">", blog.Index);
		}
	}
}
