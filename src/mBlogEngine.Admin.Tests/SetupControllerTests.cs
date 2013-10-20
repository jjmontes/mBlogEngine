using NUnit.Framework;
using mBlogEngine.Admin.Controllers;

namespace mBlogEngine.AdminWeb.Tests
{
	[TestFixture]
    public class SetupControllerTests
    {
		[Test]
		public void SetupIndex()
		{
			var controller = new SetupController();
			var actionResult = controller.Index();

			Assert.IsNotNull(actionResult);
		}
    }
}
