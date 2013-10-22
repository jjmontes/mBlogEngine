using NUnit.Framework;
using mBlogEngine.Admin.Controllers;

namespace mBlogEngine.AdminWeb.Tests
{
	[TestFixture]
	public class PostControllerTests
	{
		[Test]
		public void PostIndexView()
		{
			var controller = new PostController();
			var actionResult = controller.Index();

			Assert.IsNotNull(actionResult);
		}

		[Test]
		public void PostNewView()
		{
			var controller = new PostController();
			var actionResult = controller.New();

			Assert.IsNotNull(actionResult);
		}
	}
}
