using Xamarin.Forms;

namespace QuickContacts
{
	public partial class App : Application
	{
		public string Name { get; set; }
		public string FbId { get; set; }

		public App()
		{
			InitializeComponent();

			MainPage = new QuickContactsPage();
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
