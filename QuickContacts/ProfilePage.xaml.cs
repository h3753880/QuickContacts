using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace QuickContacts
{
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage()
		{
			InitializeComponent();

			QContactDB qcdb = new QContactDB();
			var pItems = new List<pItem>();

			string fbId = Helpers.Settings.UserId;
			string keyId = fbId + "," + fbId;
			QContact qc = qcdb.GetQContact(keyId);
			if (qc == null) qc = new QContact();
			var type = qc.GetType();
			var properties = type.GetRuntimeProperties();

			foreach (PropertyInfo prop in properties)
			{

				if (!prop.Name.Equals("myIdfriendId"))
				{
					pItem p = new pItem();
					// Add space
					if (!prop.Name.Equals("LinkedIn"))
					{
						p.pName = Regex.Replace(prop.Name, "([a-z])_?([A-Z])", "$1 $2");
					}
					else
					{
						p.pName = prop.Name;
					}

					if (p.pName.Equals("Birthday"))
					{
						if (prop.GetValue(qc, null) != null)
						{

							p.pValue = ((DateTime)prop.GetValue(qc, null)).ToLocalTime().ToString("MM/dd/yyyy");
						}
					}
					else
					{
						p.pValue = prop.GetValue(qc, null) != null ? prop.GetValue(qc, null).ToString() : "";
					}
					pItems.Add(p);
				}
			}
			pListView.ItemsSource = pItems;
			pImage.Source = ImageSource.FromUri(new Uri("http://graph.facebook.com/" + fbId + "/picture?type=small"));
		}

		public class pItem
		{
			public string pName { set; get; }
			public string pValue { set; get; }
		}

		public async void onEditClicked(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new ProfileEditPage());
		}

		public void pItemSelected(object sender, EventArgs args)
		{
			((ListView)sender).SelectedItem = null;
		}
	}
}
