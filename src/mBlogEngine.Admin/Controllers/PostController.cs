using System;
using System.Web.Mvc;
using mBlogEngine.Admin.Models;

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

		[HttpPost]
	    public ActionResult New(PostModel model)
		{
			if (ModelState.IsValid)
				return RedirectToAction("Index");
			return View();
		}
    }
}
