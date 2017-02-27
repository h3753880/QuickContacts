using System;
using System.Collections.Generic;
using System.Diagnostics;
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
					Debug.WriteLine("Answer: " + answer);
				}

				masterPage.ListView.SelectedItem = null;
				IsPresented = false;
			}
		}
	}
}
