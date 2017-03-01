using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace QuickContacts
{
	public partial class ContactsPage : ContentPage
	{
		string fbId = Helpers.Settings.UserId;
		QContactDB qcdb = new QContactDB();

		public ContactsPage()
		{
			InitializeComponent();

			var qcs = qcdb.GetQContacts(fbId);
			ShowContacts((List<QContact>)qcs);
		}

		public void ShowContacts(List<QContact> qcs)
		{
			if (qcs != null)
			{
				List<string> clist = new List<string>();

				foreach (QContact qc in qcs)
				{
					clist.Add(qc.FirstName + " " + qc.LastName);
				}

				pListView.ItemsSource = clist;
			}
		}

		public void onSearchTextChanged(object sender, EventArgs args)
		{
			var keyqcs = qcdb.GetKeyQContacts(fbId, contactSearch.Text.Trim());
			ShowContacts((List<QContact>)keyqcs);
		}

		public void onCCancelClicked(object sender, EventArgs args)
		{
			//Application.Current.MainPage = new MainPage();
		}

		public void onCExportClicked(object sender, EventArgs args)
		{
			//Application.Current.MainPage = new MainPage();
		}
	}
}
