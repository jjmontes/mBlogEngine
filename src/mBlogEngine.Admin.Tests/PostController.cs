using NUnit.Framework;
using mBlogEngine.Admin.Controllers;

namespace mBlogEngine.AdminWeb.Tests
{
	[TestFixture]
	public class PostControllerTests
	{
		[Test]
		public void PostIndex()
		{
			var controller = new PostController();
			var actionResult = controller.Index();

			Assert.IsNotNull(actionResult);
		}
	}
}
