using Xamarin.Forms;

namespace QuickContacts
{
	public partial class App : Application
	{
		const string qc_fbId = "qc_fbId";
		const string qc_name = "qc_name";

		public string Name { get; set; }
		public string FbId { get; set; }

		public App()
		{
			InitializeComponent();

			if (Properties.ContainsKey(qc_fbId))
			{
				FbId = (string)Properties[qc_fbId];
				MainPage = new ProfilePage();
			}
			else
			{
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
