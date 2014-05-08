using System.IO;
using ConsoleBlogEngine;
using NUnit.Framework;

namespace mBlogEngine.Console.Tests
{
	[TestFixture]
	public class PostCommandTests
	{
		private static void CreateFileToPost()
		{
			using (new FileStream("post.txt", FileMode.OpenOrCreate))
			{
			}
		}

		private static void PublishPost()
		{
			var command = new PublishPostCommand(ConsoleStub.WriteLine);
			command.Execute(new[] { "-f:post.txt" });
		}
		
		[Test]
		public void PublishPostWhenFileDoesntExist()
		{
			File.Delete("post.txt");
			PublishPost();

			StringAssert.Contains("cbe publish -f:post.txt", ConsoleStub.Text);
			StringAssert.Contains("File 'post.txt' doesn't exist. Try to add path to file. Example: -f:C:\\blog\\post.txt", ConsoleStub.Text);
		}

		[Test]
		public void PublishPostWhenFileExist()
		{
			File.Delete("post.txt");
			CreateFileToPost();
			PublishPost();

			StringAssert.Contains("cbe publish -f:post.txt", ConsoleStub.Text);
			StringAssert.Contains("Add file 'post.txt' to blog and publish it.", ConsoleStub.Text);
		}

		[Test]
		public void VerifyFileSystemWhenPostIsPublished()
		{
			if (Directory.Exists("blog"))
				Directory.Delete("blog", true);
			CreateFileToPost();
			PublishPost();

			Assert.IsTrue(Directory.Exists("blog"));
			Assert.IsTrue(File.Exists(@"blog\posts\post\index.html"));
		}
	}
}
