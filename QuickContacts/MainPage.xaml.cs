using System;
using System.Collections.Generic;
using System.Diagnostics;
using Facebook;
using Xamarin.Forms;

namespace QuickContacts
{
	public partial class MainPage : MasterDetailPage
	{
		public MainPage()
		{
			InitializeComponent();

			masterPage.ListView.ItemSelected += OnItemSelected;
		}

		public MainPage(string userData)
		{
			InitializeComponent();

			masterPage.ListView.ItemSelected += OnItemSelected;
		}

		//click nav
		async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MasterPageItem;

			if (item != null)
			{
				if (!item.Title.Equals("Log Out"))
				{
					// click navigation bar then go to new page (except log out)
					Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
				}
				else
				{
					var answer = await DisplayAlert("Log Out", "Are you sure to log out?", "Yes", "Cancel");
					// do log out things
					if (answer)
					{
						App app = Application.Current as App;
						FacebookClient fb = new FacebookClient();

						Helpers.Settings.UserName = string.Empty;
						Helpers.Settings.UserId = string.Empty;
						app.FbId = string.Empty;
						app.Name = string.Empty;

						var logoutUrl = fb.GetLogoutUrl(
								new { access_token = app.AccessToken, 
									  next = "https://www.facebook.com/connect/login_success.html" });

						Application.Current.MainPage = new LoginPage(logoutUrl.AbsoluteUri);
					}
				}

				masterPage.ListView.SelectedItem = null;
				IsPresented = false;
			}
		}
	}
}
