using System;
using System.Configuration;
using System.IO;
using System.Linq;
using mBlogEngine.Domain;
using mBlogEngine.FileSystem;

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
			//TODO: REFACTORIZAR ESTO!!!!
			
			_writer.Invoke(string.Format("cbe publish {0}", string.Join(" ", args)));

			var fileArg = args.SingleOrDefault(a => a.ToLowerInvariant().StartsWith("-f:"));
			if (fileArg!= null)
			{
				var fileName = fileArg.Substring(3);
				if (File.Exists(fileName))
				{
					var file = new FileInfo(fileName);
					var postTitle = file.Name;
					if (!string.IsNullOrWhiteSpace(file.Extension))
						postTitle = postTitle.Substring(0, postTitle.Length - file.Extension.Length);
					var postName = postTitle.Replace(' ', '-');

					var skeleton = new Skeleton();
					skeleton.Init();
					
					var config =
						ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap {ExeConfigFilename = "cbe.config"},
						                                                ConfigurationUserLevel.None);
					var titleBlog = string.Empty;
					var configSettings = config.AppSettings.Settings;
					if (configSettings["Blog.Title"] != null)
						titleBlog = config.AppSettings.Settings["Blog.Title"].Value;
					var blog = new Blog().SetTitle(titleBlog);

					//Blog index
					using (var stream = new StreamWriter(File.Create(@"blog\index.html")))
					{
						using (var reader = file.OpenText())
						{
							stream.Write(blog.Index);
						}
					}
					//Post file
					skeleton.InitPost(postName);
					using (var stream = new StreamWriter(File.Create(string.Format(@"blog\posts\{0}\index.html", postName))))
					{
						using (var reader = file.OpenText())
						{
							var post = blog.NewPost().SetTitle(postTitle).SetText(reader.ReadToEnd());
							stream.Write(post.Decorated);
						}
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
