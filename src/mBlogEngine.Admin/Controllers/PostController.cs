using System.Web.Mvc;

namespace mBlogEngine.Admin.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/

        public ActionResult Index()
        {
            return View();
        }

	    public ActionResult New()
	    {
		    return View();
	    }
    }
}
