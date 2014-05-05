using System.IO;
using ConsoleBlogEngine;
using NUnit.Framework;

namespace mBlogEngine.Console.Tests
{
	[TestFixture]
	public class PostCommandTests
	{
		[Test]
		public void PublishPostWhenFileDoesntExist()
		{
			var command = new PublishPostCommand(ConsoleStub.WriteLine);

			File.Delete("post.txt");
			command.Execute(new[] { "-f:post.txt" });

			StringAssert.Contains("cbe publish -f:post.txt", ConsoleStub.Text);
			StringAssert.Contains("File 'post.txt' doesn't exist. Try to add path to file. Example: -f:C:\\blog\\post.txt", ConsoleStub.Text);
		}

		[Test]
		public void PublishPostWhenFileExist()
		{
			var command = new PublishPostCommand(ConsoleStub.WriteLine);

			File.Delete("post.txt");
			using (new FileStream("post.txt", FileMode.OpenOrCreate))
			{
			}
			command.Execute(new[] { "-f:post.txt" });

			StringAssert.Contains("cbe publish -f:post.txt", ConsoleStub.Text);
			StringAssert.Contains("Add file 'post.txt' to blog and publish it.", ConsoleStub.Text);
		}

		[Test]
		public void VerifyFileSystemWhenPostIsPublished()
		{
			if (Directory.Exists("blog"))
				Directory.Delete("blog", true);
			PublishPostWhenFileExist();

			Assert.IsTrue(Directory.Exists("blog"));
			Assert.IsTrue(File.Exists(@"blog\posts\post\index.html"));
		}
	}
}
