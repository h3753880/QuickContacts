using Xamarin.Forms;

namespace QuickContacts
{
	public partial class App : Application
	{
		const string qc_fbId = "qc_fbId";
		const string qc_name = "qc_name";

		public string Name { get; set; }
		public string FbId { get; set; }
		public string AccessToken { get; set; }

		public App()
		{
			InitializeComponent();

			if (string.IsNullOrEmpty(Helpers.Settings.UserId))
			{
				MainPage = new LoginPage();
			}
			else
			{
				FbId = Helpers.Settings.UserId;
				Name = Helpers.Settings.UserName;
				MainPage = new MainPage();
			}

		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
