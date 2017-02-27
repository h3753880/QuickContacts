using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace QuickContacts
{
	public partial class MasterPage : ContentPage
	{
		public ListView ListView { get { return listView; } }

		public MasterPage()
		{
			InitializeComponent();

			//create nav item
			var masterPageItems = new List<MasterPageItem>();
			string name = Helpers.Settings.UserName;//get user name from fb
			string fbId = Helpers.Settings.UserId;//get user id from fb

			masterPageItems.Add(new MasterPageItem
			{
				Title = name,
				IconSource = "http://graph.facebook.com/" + fbId + "/picture?type=small",
				TargetType = typeof(ProfilePage)
			});

			masterPageItems.Add(new MasterPageItem
			{
				Title = "Contacts",
				IconSource = "contact.png",
				TargetType = typeof(ContactsPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "QR Code",
				IconSource = "qrcode.png",
				TargetType = typeof(QRCodePage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Log Out",
				IconSource = "logout.png",
				TargetType = typeof(LogoutPage)
			});

			listView.ItemsSource = masterPageItems;
		}
	}
}
