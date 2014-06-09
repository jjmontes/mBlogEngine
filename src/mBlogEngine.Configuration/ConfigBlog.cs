using System.Configuration;
using System.Globalization;

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

		public int Get(string key, int defaultValue = 0)
		{
			int value;
			var setting = _manager.AppSettings.Settings[key];

			if (setting == null)
				value = defaultValue;
			else
				int.TryParse(setting.Value, out value);

			return value;
		}

		public void Set(string key, string value)
		{
			if (Get(key, null) == null)
				_manager.AppSettings.Settings.Add(key, value);
			else
				_manager.AppSettings.Settings[key].Value = value;
			_manager.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
		}

		public void Set(string key, int value)
		{
			Set(key, value.ToString(CultureInfo.InvariantCulture));
		}
	}
}
