using System.IO;
using ConsoleBlogEngine;
using NUnit.Framework;

namespace mBlogEngine.Console.Tests
{
	[TestFixture]
	public class PostCommandTests
	{
		private static void ConfigureBlog(string blogTitle)
		{
			var command = new ConfigBlogCommand(ConsoleStub.WriteLine);
			command.Execute(new[] {string.Format("-ct:{0}", blogTitle.Replace(" ", "\\ "))});
		}

		private static void CreateFileToPost(string fileName)
		{
			using (var file = new StreamWriter(string.Format("{0}.txt", fileName)))
			{
				file.WriteLine("<h2>This is my first post!</h2>");
				file.WriteLine("<p>There is a paragraph</p>");
				file.WriteLine("<div>There is a div</div>");
			}
		}

		private static void PublishPost(string fileName)
		{
			var command = new PublishPostCommand(ConsoleStub.WriteLine);
			command.Execute(new[] { string.Format("-f:{0}", fileName) });
		}
		
		[Test]
		public void PublishPostWhenFileDoesntExist()
		{
			File.Delete("post.txt");
			PublishPost("post.txt");

			StringAssert.Contains("cbe publish -f:post.txt", ConsoleStub.Text);
			StringAssert.Contains("File 'post.txt' doesn't exist. Try to add path to file. Example: -f:C:\\blog\\post.txt", ConsoleStub.Text);
		}

		[Test]
		public void PublishPostWhenFileExist()
		{
			File.Delete("post.txt");
			CreateFileToPost("post");
			PublishPost("post.txt");

			StringAssert.Contains("cbe publish -f:post.txt", ConsoleStub.Text);
			StringAssert.Contains("Add file 'post.txt' to blog and publish it.", ConsoleStub.Text);
		}

		[Test]
		public void VerifyFileSystemWhenPostIsPublished()
		{
			if (Directory.Exists("blog"))
				Directory.Delete("blog", true);
			CreateFileToPost("post");
			PublishPost("post.txt");

			Assert.IsTrue(Directory.Exists("blog"));
			Assert.IsTrue(File.Exists(@"blog\posts\post\index.html"));
		}

		[Test]
		public void VerifyFileSystemWhenPostWithAnotherNameIsPublished()
		{
			if (Directory.Exists("blog"))
				Directory.Delete("blog", true);
			CreateFileToPost("my-first-post");
			PublishPost("my-first-post.txt");

			Assert.IsTrue(Directory.Exists("blog"));
			Assert.IsTrue(File.Exists(@"blog\posts\my-first-post\index.html"));
		}

		[Test]
		public void PublishPostWhenFileDoesntExistAndItsNameIsNotPost()
		{
			File.Delete("my-first-post.txt");
			PublishPost("my-first-post.txt");

			StringAssert.Contains("cbe publish -f:my-first-post.txt", ConsoleStub.Text);
			StringAssert.Contains(
				"File 'my-first-post.txt' doesn't exist. Try to add path to file. Example: -f:C:\\blog\\my-first-post.txt",
				ConsoleStub.Text);
		}

		[Test]
		public void PublishPostWhenFileExistAndItsNameIsNotPost()
		{
			File.Delete("my-first-post.txt");
			CreateFileToPost("my-first-post");
			PublishPost("my-first-post.txt");

			StringAssert.Contains("cbe publish -f:my-first-post.txt", ConsoleStub.Text);
			StringAssert.Contains("Add file 'my-first-post.txt' to blog and publish it.", ConsoleStub.Text);
		}

		[Test]
		public void PublishPostThenVerifyPostContent()
		{
			File.Delete("My First Post.txt");
			CreateFileToPost("My First Post");
			PublishPost("My First Post.txt");

			var textPost = File.ReadAllText(@"blog\posts\my-first-post\index.html");
			StringAssert.Contains("<h1>My First Post</h1>", textPost);
			StringAssert.Contains("<h2>This is my first post!</h2>", textPost);
			StringAssert.Contains("<p>There is a paragraph</p>", textPost);
			StringAssert.Contains("<div>There is a div</div>", textPost);
		}

		[Test]
		public void PublishPostWhenBlogIsNotConfiguredThenVerifyBlogIndex()
		{
			File.Delete("My First Post.txt");
			File.Delete("cbe.config");
			CreateFileToPost("My First Post");
			PublishPost("My First Post.txt");

			var blogIndex = new FileInfo(@"blog\index.html");
			Assert.IsTrue(blogIndex.Exists);
			var textBlogIndex = File.ReadAllText(@"blog\index.html");
			StringAssert.Contains("<h1></h1>", textBlogIndex);
			StringAssert.Contains("<h2>My First Post</h2>", textBlogIndex);
			StringAssert.Contains("<h5>Autor: <em>Juan Jos&eacute;</em></h5>", textBlogIndex);
			
		}

		[Test]
		public void PublishPostWhenBlogIsConfiguredThenVerifyBlogIndex()
		{
			File.Delete("My First Post.txt");
			ConfigureBlog("My blog");
			CreateFileToPost("My First Post");
			PublishPost("My First Post.txt");

			var blogIndex = new FileInfo(@"blog\index.html");
			Assert.IsTrue(blogIndex.Exists);
			var textBlogIndex = File.ReadAllText(@"blog\index.html");
			StringAssert.Contains("<h1>My blog</h1>", textBlogIndex);
			StringAssert.Contains("<h2>My First Post</h2>", textBlogIndex);
			StringAssert.Contains("<h5>Autor: <em>Juan Jos&eacute;</em></h5>", textBlogIndex);
			StringAssert.Contains("<a href=\"posts/my-first-post/index.html\"", textBlogIndex);

		}
	}
}
