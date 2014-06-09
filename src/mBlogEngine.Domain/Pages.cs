using System.Collections.Generic;

namespace mBlogEngine.Domain
{
	public class Pages
	{
		private readonly Blog _blog;
		protected IDictionary<string, Page> _pages;
		
		public Pages(Blog blog)
		{
			_blog = blog;
			_pages = new Dictionary<string, Page>
				{
					{"Index", new Index(_blog)}
				};
		}

		public Page Get(string name)
		{
			return _pages[name];
		}
	}
}