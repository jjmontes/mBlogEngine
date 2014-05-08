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
					var file = new FileInfo(fileName);
					var postName = file.Name;
					if (!string.IsNullOrWhiteSpace(file.Extension))
						postName = postName.Substring(0, postName.Length - file.Extension.Length);

					Directory.CreateDirectory("blog");
					Directory.CreateDirectory(string.Format(@"blog\posts\{0}", postName));
					using (File.Create(string.Format(@"blog\posts\{0}\index.html", postName)))
					{
						
					}
					_writer.Invoke(string.Format("Add file '{0}' to blog and publish it.", fileName));
				}
				else
				{
					_writer.Invoke(string.Format("File '{0}' doesn't exist. Try to add path to file. Example: -f:C:\\blog\\{0}",
					                             fileName));
				}
			}
		}
	}
}
