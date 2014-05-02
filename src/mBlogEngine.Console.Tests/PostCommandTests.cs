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

			command.Execute("-f:post.txt");

			StringAssert.Contains("cbe publish -f:post.txt", ConsoleStub.Text);
			StringAssert.Contains("File 'post.txt' doesn't exist. Try to add path to file. Example: -f:C:\\blog\\post.txt", ConsoleStub.Text);
		}
	}
}
