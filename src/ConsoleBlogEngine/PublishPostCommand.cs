using System;
using System.IO;
using System.Linq;

namespace ConsoleBlogEngine
{
	public class PublishPostCommand
	{
		private readonly Action<string> _writer;

		public PublishPostCommand(Action<string> writer)
		{
			_writer = writer;
		}

		public void Execute(string[] args)
		{
			_writer.Invoke(string.Format("cbe publish {0}", string.Join(" ", args)));

			var fileArg = args.SingleOrDefault(a => a.ToLowerInvariant().StartsWith("-f:"));
			if (fileArg!= null)
			{
				var fileName = fileArg.Substring(3);
				if (File.Exists(fileName))
				{
					Directory.CreateDirectory("blog");
					Directory.CreateDirectory(@"blog\posts\post");
					using (File.Create(@"blog\posts\post\index.html"))
					{
						
					}
					_writer.Invoke("Add file 'post.txt' to blog and publish it.");
				}
				else
				{
					_writer.Invoke("File 'post.txt' doesn't exist. Try to add path to file. Example: -f:C:\\blog\\post.txt");
				}
			}
		}
	}
}
