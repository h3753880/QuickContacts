using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Reflection;

namespace QuickContacts
{
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage()
		{
			InitializeComponent();

			QContactDB qcdb = new QContactDB();
			List<string> source = new List<string>();

			string fbId = Helpers.Settings.UserId;
			string keyId = fbId + "," + fbId;
			QContact qc = qcdb.GetQContact(keyId);
			if (qc == null) qc = new QContact();
			var type = qc.GetType();
			var properties = type.GetRuntimeProperties();

			foreach (PropertyInfo prop in properties)
			{
				if (prop.Name.Equals("myIdfriendId")) continue;

				source.Add(string.Format("{0} | {1}", prop.Name, prop.GetValue(qc, null)));
			}
			pListView.ItemsSource = source;
		}

		public void onEditClicked(object sender, EventArgs args)
		{
			Application.Current.MainPage = new ProfileEditPage();
		}
	}
}
