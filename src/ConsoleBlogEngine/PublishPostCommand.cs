using System;
using System.IO;
using System.Linq;
using mBlogEngine.Configuration;
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
			_writer.Invoke(string.Format("cbe publish {0}", string.Join(" ", args)));

			var fileArg = args.SingleOrDefault(a => a.ToLowerInvariant().StartsWith("-f:"));
			if (fileArg == null) return;
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

				var config = new ConfigBlog();
				Blog blog;
				InitBlog(config, out blog);
				WriteBlogPost(skeleton, config, postName, file, blog, postTitle);
				WriteBlogIndex(file, blog);
				_writer.Invoke(string.Format("Add file '{0}' to blog and publish it.", fileName));
			}
			else
			{
				_writer.Invoke(string.Format("File '{0}' doesn't exist. Try to add path to file. Example: -f:C:\\blog\\{0}",
				                             fileName));
			}
		}

		private static void InitBlog(ConfigBlog config, out Blog blog)
		{
			var titleBlog = config.Get("Blog.Title", string.Empty);
			blog = new Blog().SetTitle(titleBlog);

			var dir = new DirectoryInfo(@"blog\posts");
			foreach (var file in dir.GetFiles("index.html", SearchOption.AllDirectories))
			{
				string textFile;
				using (var stream = file.OpenText())
				{
					textFile = stream.ReadToEnd();
				}
				var startTitle = textFile.IndexOf("<h1>", StringComparison.InvariantCultureIgnoreCase) + 4;
				var lengthTitle = textFile.IndexOf("</h1>", StringComparison.InvariantCultureIgnoreCase) - startTitle;
				var title = textFile.Substring(startTitle, lengthTitle);
				blog.NewPost().SetTitle(title).Publish();
			}
			
		}

		private static void WriteBlogPost(Skeleton skeleton, ConfigBlog config, string postName, FileInfo file, Blog blog, string postTitle)
		{
			skeleton.InitPost(postName);
			using (var stream = new StreamWriter(File.Create(string.Format(@"blog\posts\{0}\index.html", postName))))
			{
				using (var reader = file.OpenText())
				{
					var post = blog.NewPost().SetTitle(postTitle).SetText(reader.ReadToEnd());
					post.Publish();
					stream.Write(post.Decorated);
				}
			}
		}

		private static void WriteBlogIndex(FileInfo file, Blog blog)
		{
			using (var stream = new StreamWriter(File.Create(@"blog\index.html")))
			{
				using (file.OpenText())
				{
					stream.Write(blog.Pages.Get("Index").Text);
				}
			}
		}
	}
}
