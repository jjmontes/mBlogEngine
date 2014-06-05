using System;
using mBlogEngine.Configuration;

namespace ConsoleBlogEngine
{
	public class ConfigBlogCommand
	{
		private readonly Action<string> _writer;

		public ConfigBlogCommand(Action<string> writer)
		{
			_writer = writer;
		}

		public void Execute(string[] args)
		{
			_writer.Invoke("cbe config blog -t:My\\ blog");

			var config = new ConfigBlog();
			config.Set("Blog.Title", "My blog");

			_writer.Invoke("Config blog: Title is \"My blog\".");
		}
	}
}
