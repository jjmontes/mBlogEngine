using System;

namespace ConsoleBlogEngine
{
	class HelpCommand
	{
		private readonly Action<string> _writer;

		public HelpCommand(Action<string> writer)
		{
			_writer = writer;
		}

		public void Execute()
		{
			_writer.Invoke("cbe help run!");
		}
	}
}
