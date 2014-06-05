using System.Configuration;

namespace mBlogEngine.Configuration
{
	public class ConfigBlog
	{
		private readonly System.Configuration.Configuration _manager;

		public ConfigBlog()
		{
			_manager =
				ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap {ExeConfigFilename = "cbe.config"},
				                                                ConfigurationUserLevel.None);
		}

		public string Get(string key, string defaultValue = null)
		{
			var setting = _manager.AppSettings.Settings[key];
			return setting == null ? defaultValue : setting.Value;
		}

		public void Set(string key, string value)
		{
			if (Get(key) == null)
				_manager.AppSettings.Settings.Add("Blog.Title", "My blog");
			else
				_manager.AppSettings.Settings["Blog.Title"].Value = "My blog";
			_manager.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
		}
	}
}
