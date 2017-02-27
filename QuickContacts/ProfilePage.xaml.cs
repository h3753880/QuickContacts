using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace QuickContacts
{
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage()
		{
			InitializeComponent();
		}

		public void onEditClicked(object sender, EventArgs args)
		{
			Application.Current.MainPage = new ProfileEditPage();
		}
	}
}
