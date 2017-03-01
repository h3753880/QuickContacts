using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace QuickContacts
{
	public partial class ContactsPage : ContentPage
	{
		string fbId = Helpers.Settings.UserId;

		public ContactsPage()
		{
			InitializeComponent();

			ShowContacts();
		}

		public void ShowContacts()
		{
			QContactDB qcdb = new QContactDB();
			var qcs = qcdb.GetQContacts(fbId);

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
