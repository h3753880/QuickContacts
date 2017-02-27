using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace QuickContacts
{
	public partial class ProfileEditPage : ContentPage
	{
		public ProfileEditPage()
		{
			InitializeComponent();
		}

		public void onPeOkClicked(object sender, EventArgs args)
		{
			Application.Current.MainPage = new MainPage();
		}

		public void onPeCancelClicked(object sender, EventArgs args)
		{
			Application.Current.MainPage = new MainPage();
		} 
	}
}
