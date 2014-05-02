using System.Text;
using ConsoleBlogEngine;
using NUnit.Framework;

namespace mBlogEngine.Console.Tests
{
    [TestFixture]
	public class HelpCommandTests
    {
	    [Test]
	    public void RunConsoleCommandWithoutParamsHelp()
	    {
		    var command = new HelpCommand(ConsoleStub.WriteLine);
			
		    command.Execute();

			StringAssert.Contains("cbe help run!", ConsoleStub.Text);
	    }
    }

	public class ConsoleStub
	{
		private static readonly StringBuilder StringBuilder = new StringBuilder();

		public static void WriteLine(string value)
		{
			StringBuilder.AppendLine(value);
		}

		public static string Text
		{
			get { return StringBuilder.ToString(); }
		}
	}
}
