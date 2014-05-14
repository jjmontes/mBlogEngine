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
	}
}
