namespace mBlogEngine.Domain
{
	public class BlogWriterDefault : IBlogWriter
	{
		public string Decorated(Post post)
		{
			return string.Format("<article><h1>{0}</h1><p>{1}</p></article>", post.Title, post.Text);
		}

		public string Decorated(Index page)
		{
			var blog = page.Blog;
			var head = string.Format("<head><title>{0}</title></head>", blog.Title);
			var posts = "";

			foreach (var post in blog.PublishedPosts)
			{
				posts += "<div class=\"col-md-4\"><h2>" + post.Title +
				         "</h2><h5>Autor: <em>Juan Jos&eacute;</em></h5><a href=\"posts/" +
				         post.Title.ToLowerInvariant().Replace(" ", "-") + "/index.html\">Read more</a></div>";
			}

			return string.Format(head + "<body><h1>{0}</h1><div class=\"container\">" + posts + "</div></body>", blog.Title);
		}
	}
}