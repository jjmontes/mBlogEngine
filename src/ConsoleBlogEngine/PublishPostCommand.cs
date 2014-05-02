using System;

namespace ConsoleBlogEngine
{
	public class PublishPostCommand
	{
		private readonly Action<string> _writer;

		public PublishPostCommand(Action<string> writer)
		{
			_writer = writer;
		}

		public void Execute(string args)
		{
			_writer.Invoke(string.Format("cbe publish {0}", args));
			_writer.Invoke("File 'post.txt' doesn't exist. Try to add path to file. Example: -f:C:\\blog\\post.txt");
		}
	}
}
