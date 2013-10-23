using System.Web;
using System.Web.Mvc;
using Moq;

namespace mBlogEngine.AdminWeb.Tests
{
	public class MvcControllerTestsBase
	{
		protected readonly Mock<HttpContextBase> HttpCtxStub = new Mock<HttpContextBase>();
		protected readonly ControllerContext ControllerCtx = new ControllerContext();

		protected void SetupTests()
		{
			ControllerCtx.HttpContext = HttpCtxStub.Object;
		}

		protected static TModel BindModel<TModel>(Controller controller, IValueProvider valueProvider) where TModel : class
		{
			var binder = ModelBinders.Binders.GetBinder(typeof (TModel));
			var bindingContext = new ModelBindingContext
				{
					FallbackToEmptyPrefix = true,
					ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof (TModel)),
					ModelName = "NotUsedButNotNull",
					ModelState = controller.ModelState,
					PropertyFilter = (name => true),
					ValueProvider = valueProvider
				};

			return (TModel) binder.BindModel(controller.ControllerContext, bindingContext);
		}
	}
}
