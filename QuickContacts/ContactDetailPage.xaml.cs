using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace QuickContacts
{
	public partial class ContactDetailPage : ContentPage
	{
		private string keyId;
		string fbId = Helpers.Settings.UserId;

		public ContactDetailPage(string keyId)
		{
			InitializeComponent();

			QContactDB qcdb = new QContactDB();
			var cdItems = new List<cdItem>();

			this.keyId = keyId;
			QContact qc = qcdb.GetQContact(keyId);
			if (qc == null) qc = new QContact();
			var type = qc.GetType();
			var properties = type.GetRuntimeProperties();

			foreach (PropertyInfo prop in properties)
			{

				if (!prop.Name.Equals("myIdfriendId"))
				{
					if (prop.Name.Equals("FirstName"))
					{
						cdLabel.Text += prop.GetValue(qc, null) != null ? prop.GetValue(qc, null).ToString() : "";
						cdLabel.Text += " ";
					}
					else if (prop.Name.Equals("LastName"))
					{
						cdLabel.Text += prop.GetValue(qc, null) != null ? prop.GetValue(qc, null).ToString() : "";
					}
					else
					{
						cdItem cd = new cdItem();
						// Add space
						if (!prop.Name.Equals("LinkedIn"))
						{
							cd.cdName = Regex.Replace(prop.Name, "([a-z])_?([A-Z])", "$1 $2");
						}
						else
						{
							cd.cdName = prop.Name;
						}

						// Not show the time, only show date
						if (cd.cdName.Equals("Birthday"))
						{
							if (prop.GetValue(qc, null) != null)
							{

								cd.cdValue = ((DateTime)prop.GetValue(qc, null)).ToLocalTime().ToString("MM/dd/yyyy");
							}
						}
						else
						{
							cd.cdValue = prop.GetValue(qc, null) != null ? prop.GetValue(qc, null).ToString() : "";
						}
						cdItems.Add(cd);
					}
				}
			}
			cdListView.ItemsSource = cdItems;
			cdImage.Source 
			       = ImageSource.FromUri(new Uri("http://graph.facebook.com/" + qc.myIdfriendId.Split(',')[1] + "/picture?type=small"));
		}

		public class cdItem
		{
			public string cdName { set; get; }
			public string cdValue { set; get; }
		}

		public void cdItemSelected(object sender, EventArgs args)
		{
			((ListView)sender).SelectedItem = null;
		}

		public void onCdDeleteClicked(object sender, EventArgs args)
		{
			QContactDB qcdb = new QContactDB();
			qcdb.DeleteQContact(keyId);

			// Sent message to inform Contacts Page to delete the item
			MessagingCenter.Send<ContactDetailPage, string>
				(this, "DeleteInformation", keyId);

			// cause android crash, seeking solutions
			// Page page = Navigation.NavigationStack.First();
			// Navigation.RemovePage(page);
			Navigation.PopAsync();
		}

		public async void onCdExportClicked(object sender, EventArgs args)
		{
			// export data to mobile contacts
			// export data to contacts
			IAddContactsInfo addContacts = DependencyService.Get<IAddContactsInfo>();
			QContactDB qcdb = new QContactDB();
			QContact qc = qcdb.GetQContact(keyId);

			Debug.WriteLine(qc.FirstName);
			bool exResult = addContacts.AddContacts(qc);

			if (exResult)
				await DisplayAlert("Confirm", "The contact information has been sucessfully exported", "OK");
			else
				await DisplayAlert("ERROR", "Exporting Fail", "OK");
			// cause android crash, seeking solutions
			// Page page = Navigation.NavigationStack.First();
			// Navigation.RemovePage(page);
			await Navigation.PopAsync();
		}

	}
}
