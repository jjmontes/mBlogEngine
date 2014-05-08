using System.IO;
using ConsoleBlogEngine;
using NUnit.Framework;

namespace mBlogEngine.Console.Tests
{
	[TestFixture]
	public class PostCommandTests
	{
		private static void CreateFileToPost(string fileName)
		{
			using (new FileStream(string.Format("{0}", fileName), FileMode.OpenOrCreate))
			{
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
			CreateFileToPost("post.txt");
			PublishPost("post.txt");

			StringAssert.Contains("cbe publish -f:post.txt", ConsoleStub.Text);
			StringAssert.Contains("Add file 'post.txt' to blog and publish it.", ConsoleStub.Text);
		}

		[Test]
		public void VerifyFileSystemWhenPostIsPublished()
		{
			if (Directory.Exists("blog"))
				Directory.Delete("blog", true);
			CreateFileToPost("post.txt");
			PublishPost("post.txt");

			Assert.IsTrue(Directory.Exists("blog"));
			Assert.IsTrue(File.Exists(@"blog\posts\post\index.html"));
		}

		[Test]
		public void VerifyFileSystemWhenPostWithAnotherNameIsPublished()
		{
			if (Directory.Exists("blog"))
				Directory.Delete("blog", true);
			CreateFileToPost("my-first-post.txt");
			PublishPost("my-first-post.txt");

			Assert.IsTrue(Directory.Exists("blog"));
			Assert.IsTrue(File.Exists(@"blog\posts\my-first-post\index.html"));
		}
	}
}
