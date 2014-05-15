using System;

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
			_writer.Invoke("Config blog: Title is \"My blog\".");
		}
	}
}
