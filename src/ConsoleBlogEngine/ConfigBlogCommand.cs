using System;
using System.Configuration;

namespace ConsoleBlogEngine
{
	public class ConfigBlogCommand
	{
		private readonly Action<string> _writer;

		public ConfigBlogCommand(Action<string> writer)
		{
			_writer = writer;
		}

		public void Execute(string[] args)
		{
			_writer.Invoke("cbe config blog -t:My\\ blog");

			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			config.AppSettings.Settings.Add("Blog.Title", "My blog");
			config.SaveAs("cbe.config", ConfigurationSaveMode.Modified);

			ConfigurationManager.RefreshSection("appSettings");
			_writer.Invoke("Config blog: Title is \"My blog\".");
		}
	}
}
