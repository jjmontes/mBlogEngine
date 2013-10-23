using System.Web.Mvc;
using NUnit.Framework;
using mBlogEngine.Admin.Controllers;
using mBlogEngine.Admin.Models;

namespace mBlogEngine.AdminWeb.Tests
{
	[TestFixture]
	public class PostControllerTests : MvcControllerTestsBase
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

		[Test]
		public void PostNewViewIncompleteModel()
		{
			var controller = new PostController {ControllerContext = ControllerCtx};
			var form = new FormCollection
				{
					{"Title", ""},
					{"Text", ""}
				};
			var postNew = BindModel<PostModel>(controller, form);
			var actionResult = controller.New(postNew);

			Assert.IsNotNull(actionResult);
			Assert.AreEqual(typeof(ViewResult), actionResult.GetType());
		}

		[Test]
		public void PostNewViewCompleteModel()
		{
			var controller = new PostController { ControllerContext = ControllerCtx };
			var form = new FormCollection
				{
					{"Title", "Hello world"},
					{"Text", "This is the first post."}
				};
			var postNew = BindModel<PostModel>(controller, form);
			var actionResult = controller.New(postNew);

			Assert.IsNotNull(actionResult);
			Assert.AreEqual(typeof(RedirectToRouteResult), actionResult.GetType());
		}
	}
}
