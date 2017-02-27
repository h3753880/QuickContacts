using System;
using System.Collections.Generic;

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

			masterPageItems.Add(new MasterPageItem
			{
				Title = "Bob Stone",
				IconSource = "profile.jpg",
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
