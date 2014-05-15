using ConsoleBlogEngine;
using NUnit.Framework;

namespace mBlogEngine.Console.Tests
{
	[TestFixture]
	public class BlogCommandTests
	{
		[Test]
		public void ConfigureBlogTitle()
		{
			var command = new ConfigBlogCommand(ConsoleStub.WriteLine);
			command.Execute(new[] { "-ct:My\\ blog"});

			StringAssert.Contains("cbe config blog -t:My\\ blog", ConsoleStub.Text);
			StringAssert.Contains("Config blog: Title is \"My blog\".", ConsoleStub.Text);
		}
	}
}
