// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace QuickContacts.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

    #region Setting Constants
    private const string SettingsKey = "settings_key";
    private static readonly string SettingsDefault = string.Empty;
    #endregion


    public static string UserName
    {
      get
      {
        return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
      }
      set
      {
        AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
      }
    }

		#region Setting Constants
		private const string SettingsKey2 = "settings_key2";
		private static readonly string SettingsDefault2 = string.Empty;
		#endregion

	public static string UserId
	{
		get
		{
			return AppSettings.GetValueOrDefault<string>(SettingsKey2, SettingsDefault2);
		}
		set
		{
			AppSettings.AddOrUpdateValue<string>(SettingsKey2, value);
		}
	}

		#region Setting Constants
		private const string SettingsKey3 = "settings_key3";
		private static readonly string SettingsDefault3 = string.Empty;
		#endregion

		public static string QRData
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(SettingsKey3, SettingsDefault3);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(SettingsKey3, value);
			}
		}
  }
}